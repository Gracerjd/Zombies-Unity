using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    private int hp;
    private int maxhp;
    private bool isDead;
    private static MainTower instance;
    public static MainTower Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    public void UpdateHp(int hp,int maxhp)
    {
        this.hp = hp;
        this.maxhp = maxhp;

        UIMgr.Instance.GetPanel<GamePanel>().UpdataHp(hp,maxhp);
    }
    public void Wound(int dmg)
    {
        if (isDead) 
            return; 
        hp-=dmg;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            GameLosePanel gameLosePanel = UIMgr.Instance.ShowPanel<GameLosePanel>();
            gameLosePanel.InitInfo((int)(GameLevelMgr.Instance.playerObj.money * 0.3f),false);
        }
        UpdateHp(hp,maxhp);
    }
    private void OnDestroy()
    {
        //过场景要将instance置空，
        instance = null;
    }

    
    
}
