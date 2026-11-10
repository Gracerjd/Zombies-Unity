using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLosePanel : BasePanel
{
    public Text txtTitle;
    public Text txtContent;
    public Text txtMoney;

    public Button btnSure;

    public override void Init()
    {
        btnSure.onClick.AddListener(() => {
            //隐藏面板 
            UIMgr.Instance.HidePanel<GameLosePanel>();
            UIMgr.Instance.HidePanel<GamePanel>();
            //清空当前关卡的数据
            GameLevelMgr.Instance.ClearInfo();
            //切换场景
            SceneManager.LoadScene("BeginScene");
        });
    }

    public void InitInfo(int money, bool isWin)
    {
        txtTitle.text = isWin ? "通关" : "失败";
        txtContent.text = isWin ? "获得胜利奖励" : "获得失败奖励";
        txtMoney.text = "$" + money;

        //根据奖励 改变玩家数据
        GameDataMgr.Instance.playerData.haveMoney += money;
        GameDataMgr.Instance.SavePlayerData();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Cursor.lockState = CursorLockMode.None;
    }
}