using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    //造塔点的塔对象
    private GameObject towerObj;
    //当前塔的信息
    public TowerInfo nowtowerInfo;
    //有多少个塔
    public List<int> nowtowerIDs;

    public int nowLevelIndex;
    public void CreateTower(int id,int levelIndex)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfos[id - 1];
        TowerLevelInfo towerLevel= info.levels[levelIndex];
        if (towerLevel.money > GameLevelMgr.Instance.playerObj.money)
        {
            return;
        }
        //扣钱
        GameLevelMgr.Instance.playerObj.AddMoney(-towerLevel.money);
        //创建塔
        if (towerObj!=null)
        {
            Destroy(towerObj);
            towerObj = null;

        }
        towerObj = Instantiate(Resources.Load<GameObject>(towerLevel.res),this.transform.position,Quaternion.identity);
        //初始化
        towerObj.GetComponent<TowerObj>().InitInfo(info,towerLevel);

        //记录当前塔的数据
        //info 是 CreateTower 方法里的局部变量，方法执行完就没了。所以在这用一个变量记录
        nowtowerInfo = info;
        nowLevelIndex = levelIndex;
        //更新塔的UI信息
        UIMgr.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
        
    }

    private void OnTriggerEnter(Collider other)
    {

        UIMgr.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
    }

    private void OnTriggerExit(Collider other)
    {
        
        UIMgr.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
    }
}
