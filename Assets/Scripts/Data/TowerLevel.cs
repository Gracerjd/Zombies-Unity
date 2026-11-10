using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一个塔的等级信息以及数据
/// </summary>
public class TowerLevelInfo
{
    public int level;
    public int money;
    public int atk;
    public int atkRange;
    public float offsetTime;
    public string imgRes;
    public string res;
    public string eff;
}
/// <summary>
/// 所有的塔的总类
/// </summary>
public class TowerInfo
{
    public int id;
    public string name;
    public int type;
    public List<TowerLevelInfo> levels;
}
