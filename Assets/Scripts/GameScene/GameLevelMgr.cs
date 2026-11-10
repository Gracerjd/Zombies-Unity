using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance=new GameLevelMgr();
    public static GameLevelMgr Instance => instance;

    public PlayerObj playerObj;
    //出怪点列表
    private List<MonsterPoint> pointList=new List<MonsterPoint>();
    //怪物的列表
    private List<MonsterObj> monsterObjList=new List<MonsterObj>();
    private int nowWaveNum = 0;
    private int maxWaveNum = 0;
    
    private GameLevelMgr() 
    { 
    
    }

    public void InitInfo(SceneInfo info)
    {
        UIMgr.Instance.ShowPanel<GamePanel>();

        RoleInfo nowRole = GameDataMgr.Instance.nowRole;

        Transform heroPos = GameObject.Find("PlayerBorn").transform;
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(nowRole.res),heroPos.position,heroPos.rotation);
        //玩家的初始化
        playerObj=heroObj.GetComponent<PlayerObj>();
        playerObj.InitPlayerInfo(nowRole.atk,400);
        //摄像机设置看向对象
        Camera.main.GetComponent<CameraMove>().SetTargetPos(heroObj.transform);
        //设置游戏ui的血条
        MainTower.Instance.UpdateHp(info.towerHp,info.towerHp);
    }
    public void AddMonsterPoint(MonsterPoint monsterPoint)
    {
        pointList.Add(monsterPoint);
    }
    /// <summary>
    /// 更新最大波次
    /// </summary>
    /// <param name="num">多少波次的怪</param>
    public void UpdateMaxWave(int num)
    {
        maxWaveNum += num;
        nowWaveNum = maxWaveNum;
        UIMgr.Instance.GetPanel<GamePanel>().UpdateBoci(nowWaveNum, maxWaveNum);
    }

    public void ChangeNowWaveNum(int num)
    {
        nowWaveNum -= num;
        UIMgr.Instance.GetPanel<GamePanel>().UpdateBoci(nowWaveNum, maxWaveNum);
    }
    public void ChangeMonsterNum(MonsterObj monsterObj)
    {
        monsterObjList.Add(monsterObj);
    }

    public void RemoveMonsterNum(MonsterObj monsterObj)
    {
        monsterObjList.Remove(monsterObj);
    }

    public MonsterObj FindMonster(Vector3 pos, int range)
    {
        for (int i = 0; i < monsterObjList.Count; i++)
        {
            //怪物没死并且在射程范围内
            if (!monsterObjList[i].isDead && Vector3.Distance(pos, monsterObjList[i].transform.position) <= range)
            {
                return monsterObjList[i];
            }
        }
        return null;
    }

    public List<MonsterObj> FindMonsterList(Vector3 pos,int range)
    {
        List<MonsterObj> list= new List<MonsterObj>();
        for (int i = 0; i < monsterObjList.Count; i++)
        {
            if (!monsterObjList[i].isDead && Vector3.Distance(pos, monsterObjList[i].transform.position) <= range)
            {
                list.Add(monsterObjList[i]);
            }
        }
        return list;
    }
    /// <summary>
    /// 检测是否胜利的条件 1所有出怪点没有怪物了 2当前场景怪物没有了
    /// </summary>
    /// <returns></returns>
    public bool CheckOver()
    {
        for (int i = 0; i < pointList.Count; i++)
        {
            //判断出怪点是否出完怪
            if (!pointList[i].CheckOver())
            {
                return false;
            }
        }

        if (monsterObjList.Count>0)
        {
            return false;
        }
        
        return true;
    }

    public void ClearInfo()
    {
        pointList.Clear();
        monsterObjList.Clear();
        nowWaveNum= maxWaveNum=  0;
        playerObj = null;
    }

    
    
}
