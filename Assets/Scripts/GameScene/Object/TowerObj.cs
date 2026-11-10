using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObj : MonoBehaviour
{
    public Transform head;
    public Transform firePoint;
    private float roundSpeed=150;
    private MonsterObj target;
    private List<MonsterObj> targetList;
    private TowerLevelInfo towerLevelInfo;
    private TowerInfo towerInfo;
    private float nowTime;
    private Vector3 monsterPos;

    public void InitInfo(TowerInfo info,TowerLevelInfo levelInfo)
    {
        this.towerInfo = info;
        this.towerLevelInfo = levelInfo;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (towerInfo.type==1)
        {
            //怪物空了，或者死亡了，或者跑出射程就重新寻找怪物
            if(target == null || target.isDead  || Vector3.Distance(this.transform.position, target.transform.position) > towerLevelInfo.atkRange)
            {
                target= GameLevelMgr.Instance.FindMonster(this.transform.position, towerLevelInfo.atkRange);
            }
            if (target == null)
                return;
            monsterPos = target.transform.position;
            monsterPos.y = head.position.y;
            head.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(monsterPos - head.position),roundSpeed*Time.deltaTime);

            if (Vector3.Angle(head.forward,monsterPos-head.position)<5&&Time.time-nowTime> towerLevelInfo.offsetTime)
            {
                //让目标受伤
                target.Wound(towerLevelInfo.atk);
                //播放音效
                GameDataMgr.Instance.PlaySound("Music/Tower");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(towerLevelInfo.eff),firePoint.position,firePoint.rotation);
                Destroy(effObj,0.2f);

                nowTime = Time.time;
            }
        }
        else
        {
            targetList = GameLevelMgr.Instance.FindMonsterList(this.transform.position, towerLevelInfo.atkRange);
            if (targetList.Count > 0 && Time.time - nowTime > towerLevelInfo.offsetTime)
            {
                //播放音效
                //GameDataMgr.Instance.PlaySound("Music");

                GameObject effObj = Instantiate(Resources.Load<GameObject>(towerLevelInfo.eff), firePoint.position, firePoint.rotation);
                Destroy(effObj, 0.2f);
                //让目标受伤
                for (int i = 0; i < targetList.Count; i++)
                {
                    targetList[i].Wound(towerLevelInfo.atk);
                }
                nowTime = Time.time;
            }
        }
    }
}
