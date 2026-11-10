using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoosePanel : BasePanel
{
    public Text txtRole;
    public Text txtMoneyMount;
    public Text txtUnlock;
    public Button btnLeft;
    public Button btnRight;
    public Button btnBack;
    public Button btnStart;
    public Button btnUnlock;

    private Transform HeroPos;
    //记录玩家选的角色
    private GameObject heroObj;
    //当前角色的信息
    private RoleInfo nowRoleInfo;
    //当前玩家的角色
    private int nowID;
    public override void Init()
    {
        HeroPos = GameObject.Find("HeroPos").transform;
        //更新钱的文本
        txtMoneyMount.text = GameDataMgr.Instance.playerData.haveMoney.ToString();
        btnStart.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.nowRole = nowRoleInfo;
            UIMgr.Instance.HidePanel<ChoosePanel>();
            UIMgr.Instance.ShowPanel<ChooseScenePanel>();
        });
        btnBack.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<ChoosePanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnRight(() =>
            {
                UIMgr.Instance.ShowPanel<BeginPanel>();
            });
        });
        btnLeft.onClick.AddListener(() =>
        {
            --nowID;
            if (nowID < 0)
                nowID = GameDataMgr.Instance.roleInfoLists.Count-1;
            ChangeHero();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++nowID;
            if (nowID >= GameDataMgr.Instance.roleInfoLists.Count)
                nowID = 0;
            ChangeHero();
        });
        btnUnlock.onClick.AddListener(() =>
        {
            PlayerData playerData = GameDataMgr.Instance.playerData;
            if (playerData.haveMoney>=nowRoleInfo.lockMoney)
            {
                playerData.haveMoney -= nowRoleInfo.lockMoney;
                txtMoneyMount.text=playerData.haveMoney.ToString();
                //记录购买的id
                playerData.buyHeroList.Add(nowRoleInfo.id);
                //保存数据
                GameDataMgr.Instance.SavePlayerData();
                UpdateLockBtn();
                //提示面板
                UIMgr.Instance.ShowPanel<TigPanel>().ChangeInfo("购买成功");
            }
            else
            {
                //提示面板购买失败
                UIMgr.Instance.ShowPanel<TigPanel>().ChangeInfo("金钱不足");
            }
        });
        //一开始
        ChangeHero();
    }
    
    private void ChangeHero()
    {
        if (heroObj!=null)
        {
            Destroy(heroObj);
            heroObj = null;
        }
        nowRoleInfo=GameDataMgr.Instance.roleInfoLists[nowID];
        txtRole.text = nowRoleInfo.tips;
        heroObj = Instantiate(Resources.Load<GameObject>(nowRoleInfo.res),HeroPos);
        //因为预设体挂载了脚本在这需删除
        Destroy(heroObj.GetComponent<PlayerObj>());

        UpdateLockBtn();
    }
    /// <summary>
    /// 更新解锁按钮
    /// </summary>
    private void UpdateLockBtn()
    {
        if (nowRoleInfo.lockMoney > 0 && !GameDataMgr.Instance.playerData.buyHeroList.Contains(nowRoleInfo.id))
        {
            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "$" + nowRoleInfo.lockMoney;
            btnStart.gameObject.SetActive(false);
        }
        else 
        {
            btnUnlock.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }
    }
    //在方法体里重写Destroy函数
    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if (heroObj!=null)
        {
            DestroyImmediate(heroObj);
            heroObj = null;
        }
    }
}
