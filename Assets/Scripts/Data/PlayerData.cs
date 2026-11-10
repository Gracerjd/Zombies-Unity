using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家的游玩记录数据
/// </summary>
public class PlayerData
{
    public int haveMoney = 200;
    //记录购买过的角色
    public List<int> buyHeroList= new List<int>();
}
