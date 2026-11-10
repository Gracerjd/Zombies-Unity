using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    private Animator animator;
    //玩家属性
    public int atk;
    public int money;
    private float roundSpeed=50;

    
    //枪开火点
    public Transform firePos;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }
    public void InitPlayerInfo(int atk,int money)
    {
        this.atk = atk;
        this.money = money;

        UpdateMoney();
    }
    // Update is called once per frame
    void Update()
    {
        //位移
        animator.SetFloat("VSpeed",Input.GetAxis("Vertical"));
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        //旋转
        this.transform.Rotate(Vector3.up,Input.GetAxis("Mouse X")*roundSpeed*Time.deltaTime);
        //蹲下
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetLayerWeight(1, 1);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetLayerWeight(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("Roll");
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        
    }
    public void KnifeEvent()
    {
        Collider[] colliders= Physics.OverlapSphere(this.transform.position+this.transform.forward+this.transform.up,1,1<<LayerMask.NameToLayer("Monster"));
        GameDataMgr.Instance.PlaySound("Music/Knife");
        for (int i=0; i<colliders.Length; i++)
        {
            //怪物受伤逻辑
            MonsterObj monsterObj = colliders[i].gameObject.GetComponent<MonsterObj>();
            if (monsterObj!=null&&!monsterObj.isDead)
            {
                
                monsterObj.Wound(atk);
                break;
            }
        }
    }
    public void ShootEvent()
    {
        RaycastHit[] raycastHits= Physics.RaycastAll(new Ray(firePos.position,this.transform.forward),1000,1<<LayerMask.NameToLayer("Monster"));
        //该射线检测判断返回的是单根射线碰撞到的信息
        //if(Physics.Raycast(new Ray(firePos.position,this.transform.forward),out RaycastHit hitInfo,100,1<<LayerMask.NameToLayer("Monster")))
        GameDataMgr.Instance.PlaySound("Music/Gun");
        for (int i = 0; i < raycastHits.Length; i++)
        {
            //怪物受伤逻辑
            MonsterObj monsterObj = raycastHits[i].collider.gameObject.GetComponent<MonsterObj>();
            if (monsterObj != null && !monsterObj.isDead)
            {
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.nowRole.FireEff));
                effObj.transform.position = raycastHits[i].transform.position;
                effObj.transform.rotation = Quaternion.LookRotation(raycastHits[i].normal);
                

                monsterObj.Wound(atk);
                break;
            }
        }
    }
    /// <summary>
    /// 更新ui上的钱
    /// </summary>
    public void UpdateMoney()
    {
        UIMgr.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }
    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }
}
