using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    //波次最大上限
    public int maxWave;
    //一个波次的怪物数量
    public int monsterNumOneWave;
    //当前的怪物数量
    private int nowNum;
    //怪物ID列表
    public List<int> mosterIds;
    //当前怪物ID
    private int nowID;
    //怪物之间的创造间隔
    public float createOffset;
    //波次之间的间隔
    public float waveOffset;
    //第一波次的开始间隔时间
    public float firstWaveOffset;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateWave", firstWaveOffset);
        GameLevelMgr.Instance.AddMonsterPoint(this);
        GameLevelMgr.Instance.UpdateMaxWave(maxWave);
    }
    private void CreateWave()
    {
        nowID = mosterIds[Random.Range(0, mosterIds.Count)];
        nowNum = monsterNumOneWave;
        CreateMonster();
        --maxWave;
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }
    private void CreateMonster()
    {
        MonsterInfo monsterInfo = GameDataMgr.Instance.monsterInfos[nowID - 1];
        GameObject gameObject = Instantiate(Resources.Load<GameObject>(monsterInfo.res),this.transform.position,Quaternion.identity);
        MonsterObj monsterObj = gameObject.AddComponent<MonsterObj>();
        monsterObj.InitInfo(monsterInfo);

        //GameLevelMgr.Instance.ChangeMonsterNum(1);
        GameLevelMgr.Instance.ChangeMonsterNum(monsterObj);

        --nowNum;
        if (nowNum==0)
        {
            if (maxWave>0)
            {
                Invoke("CreateWave", waveOffset);
                
            }
            
        }
        else
        {
            Invoke("CreateMonster", createOffset);
        }
    }
    public bool CheckOver()
    {
        return nowNum == 0 && maxWave == 0;
    }
}
