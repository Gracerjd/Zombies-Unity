using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObj : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private MonsterInfo monsterInfo;
    private int hp;
    //死亡标识
    public bool isDead;

    private float frontAtkTime;
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }
    //提供给外部的初始化
    public void InitInfo(MonsterInfo monsterInfo)
    {
        this.monsterInfo = monsterInfo;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(monsterInfo.animator);
        hp=monsterInfo.hp;
        //速度和加速度的赋值一样是因为 希望没有明显的加速运动，加速度小，先慢后快，如果比较大就会很快变成匀速运动
        agent.speed = agent.acceleration = monsterInfo.moveSpeed;
        agent.angularSpeed = monsterInfo.roundSpeed;
    }
    public void Wound(int dmg)
    {
        if (isDead)
            return;
        hp-=dmg;
        animator.SetTrigger("Wound");
        if (hp<=0)
        {
            //死亡
            Dead();
        }
        else
        {
            //受伤音效
            GameDataMgr.Instance.PlaySound("Music/Wound");
        }
    }
    public void Dead()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("Dead",true);
        //播放音效
        GameDataMgr.Instance.PlaySound("Music/dead");
        //加钱
        GameLevelMgr.Instance.playerObj.AddMoney(50);
    }
    public void DeadEvent()
    {
        //GameLevelMgr.Instance.ChangeMonsterNum(-1);
        GameLevelMgr.Instance.RemoveMonsterNum(this);

        Destroy(this.gameObject);
        if (GameLevelMgr.Instance.CheckOver())
        {
            GameLosePanel gameLosePanel = UIMgr.Instance.ShowPanel<GameLosePanel>();
            gameLosePanel.InitInfo((int)(GameLevelMgr.Instance.playerObj.money * 0.3f), true);
        }
        
    }
    public void BornOver()
    {
        agent.SetDestination(MainTower.Instance.transform.position);
        animator.SetBool("Run",true);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        animator.SetBool("Run", agent.velocity!=Vector3.zero);
        //Time.time 是游戏运行的时间
        if (Vector3.Distance(this.transform.position,MainTower.Instance.transform.position)<6&& Time.time-frontAtkTime>=monsterInfo.atkOffst)
        {
            frontAtkTime = Time.time;
            animator.SetTrigger("Atk"); 
        }
    }

    public void AtkEvent()
    {
        Collider[] colliders= Physics.OverlapSphere(this.transform.position+this.transform.forward+this.transform.up,1,1<<LayerMask.NameToLayer("MainTower"));
        GameDataMgr.Instance.PlaySound("Music/Eat");
        for (int i = 0; i < colliders.Length; i++)
        {
            if (MainTower.Instance.gameObject == colliders[i].gameObject)
            {
                MainTower.Instance.Wound(monsterInfo.atk);
            }
            
        }
    }
}
