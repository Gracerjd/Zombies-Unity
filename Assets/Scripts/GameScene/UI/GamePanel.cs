using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHp;
    public Text txtHp;
    public Text txtboci;
    public Text txtMoneyMount;
    public float hpW=460f;
    public Button btnESC;
    //造塔对象组合控件的父对象，用于控制显隐
    public Transform towerFatherTrans;
    //造塔按钮的列表
    public List<TowerBtn> towerBtn=new List<TowerBtn>();
    //当前的造塔点
    private TowerPoint nowSelTowerPoint;
    //用来检测输入
    private bool checkInput;
    public override void Init()
    {
        Cursor.lockState = CursorLockMode.Confined;
        btnESC.onClick.AddListener(() =>
        {
            // 先关闭玩家输入
            if (GameLevelMgr.Instance.playerObj != null)
            {
                GameLevelMgr.Instance.playerObj.gameObject.SetActive(false);
            }

            // 恢复鼠标
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 清理当前关卡状态
            GameLevelMgr.Instance.ClearInfo();

            UIMgr.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });
        towerFatherTrans.gameObject.SetActive(false);
    }
    
    public void UpdataHp(int hp,int maxHp)
    {
        txtHp.text = hp + "/" + maxHp;
        //不能用整数进行除法会向下取整变成0
        (imgHp.transform as RectTransform).sizeDelta = new Vector2((float)hp/maxHp*hpW,33);
    }
    
    public void UpdateBoci(int nowBoci,int maxBoci)
    {
        txtboci.text=nowBoci+"/"+maxBoci;

    }
    public void UpdateMoney(int money)
    {
        txtMoneyMount.text=money.ToString();
        if (nowSelTowerPoint!=null)
        {
            UpdateSelTower(nowSelTowerPoint);
        }
    }

    public void UpdateSelTower(TowerPoint towerPoint)
    {
        nowSelTowerPoint = towerPoint;
        if (nowSelTowerPoint==null)
        {
            checkInput = false;
            towerFatherTrans.gameObject.SetActive(false);
        }
        else
        {
            checkInput=true;
            towerFatherTrans.gameObject.SetActive (true);
            if (nowSelTowerPoint.nowtowerInfo == null)
            {
                //当前没有塔建造时
                for (int i = 0; i < towerBtn.Count; i++)
                {
                    towerBtn[i].gameObject.SetActive(true);
                    towerBtn[i].InitInfo(nowSelTowerPoint.nowtowerIDs[i], "数字键" + (i + 1),0);
                }
            }
            //已经有塔建造了
            else
            {
                TowerInfo current = nowSelTowerPoint.nowtowerInfo;
                int nextlevelIndex = nowSelTowerPoint.nowLevelIndex + 1;
                // 已经满级，隐藏建造/升级面板
                if (nextlevelIndex > current.levels.Count-1)
                {
                    towerFatherTrans.gameObject.SetActive(false);
                    checkInput = false;
                    return;
                }

                // 只显示第一张卡，作为升级按钮
                for (int i = 0; i < towerBtn.Count; i++)
                {
                    towerBtn[i].gameObject.SetActive(i == 0);
                }

                towerBtn[0].InitInfo(current.id, "空格升级" , nextlevelIndex);
            }
        }

        
    }
    protected override void Update()
    {
        base.Update();
        if (!checkInput)
            return;
        if (nowSelTowerPoint==null)
        {
            checkInput = false;
            towerFatherTrans.gameObject.SetActive(false);
            return;
        }
        if (nowSelTowerPoint.nowtowerInfo==null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowtowerIDs[0],0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowtowerIDs[1],0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowtowerIDs[2],0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowtowerInfo.id,nowSelTowerPoint.nowLevelIndex+1);
            }
        }
    }
}
