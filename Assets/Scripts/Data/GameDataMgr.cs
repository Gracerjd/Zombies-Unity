using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用来管理数据类
/// </summary>
public class GameDataMgr
{
    private static GameDataMgr instance=new GameDataMgr();
    public static GameDataMgr Instance => instance;
    //用于记录面板场景角色的选择
    public RoleInfo nowRole;

    //提供给外部进行调用的数据
    public MusicData musicData;
    //主界面玩家数据
    public PlayerData playerData;
    //角色的列表
    public List<RoleInfo> roleInfoLists;
    //场景的列表
    public List<SceneInfo> sceneInfoLists;
    //怪物的列表
    public List<MonsterInfo> monsterInfos;
    //塔的列表
    public List<TowerInfo> towerInfos;
    private GameDataMgr() 
    {
        //初始化
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");

        roleInfoLists = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");

        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");

        sceneInfoLists = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");

        monsterInfos = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");

        towerInfos = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerLevel");
    }
    /// <summary>
    /// 外部调用的 存储音乐数据的方法
    /// </summary>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData,"MusicData");
    }
    /// <summary>
    /// 存储玩家数据的方法
    /// </summary>
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }

    public void PlaySound(string resName)
    {
        GameObject musicObj = new GameObject();
        AudioSource a= musicObj.AddComponent<AudioSource>();
        a.clip = Resources.Load<AudioClip>(resName);
        a.volume = musicData.soundValue;
        a.mute = !musicData.isOpenSound;
        a.Play();
        GameObject.Destroy(musicObj,1);
    }
}
