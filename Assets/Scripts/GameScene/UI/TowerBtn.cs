using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    public Image imgTower;
    public Text txtInfo;
    public Text txtMoney;
    /// <summary>
    /// 初始化塔按钮
    /// </summary>
    /// <param name="towerID">塔的id数</param>
    /// <param name="inputStr">修改塔按钮的文本</param>
    /// <param name="levelIndex">等级索引（id不同 等级也有不同的，不能只用id）</param>
    public void InitInfo(int towerID,string inputStr,int levelIndex)
    {
        if (towerID > GameDataMgr.Instance.towerInfos.Count || towerID <= 0)
        {
            return;
        }
        TowerInfo towerInfo= GameDataMgr.Instance.towerInfos[towerID-1];
        if (towerInfo == null||levelIndex < 0 || towerInfo.levels == null|| levelIndex>=towerInfo.levels.Count )
            return;
        TowerLevelInfo towerLevel = towerInfo.levels[levelIndex];
        imgTower.sprite=Resources.Load<Sprite>(towerLevel.imgRes);
        txtInfo.text=inputStr;
        txtMoney.text = "$"+ towerLevel.money;
        int money = GameLevelMgr.Instance.playerObj != null
        ? GameLevelMgr.Instance.playerObj.money: 0;

        if (towerLevel.money > money)
        {
            txtMoney.text = "金钱不足";
        }

    }
}
