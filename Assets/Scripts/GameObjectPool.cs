using System;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPoolManager : MonoBehaviour
{
    //单例
    public static ObjectPoolManager Instance { get; private set; }

    // 字典：预制体 -> 对应的对象池队列
    private Dictionary<GameObject, Queue<GameObject>> poolDic = new Dictionary<GameObject, Queue<GameObject>>();

    // 池子物体统一放在这个父节点，层级整洁
    private Transform poolRoot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 创建一个子物体，所有回收的对象都放在这里
        GameObject rootObj = new GameObject("PoolRoot");
        rootObj.transform.SetParent(transform);
        poolRoot = rootObj.transform;
    }

    /// <summary>
    /// 从池子拿物体，传入你要生成的预制体
    /// </summary>
    public GameObject Get(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("预制体为空！");
            return null;
        }

        // 如果字典没有这个预制体的池子，新建池子
        if (!poolDic.ContainsKey(prefab))
        {
            poolDic.Add(prefab, new Queue<GameObject>());
        }

        Queue<GameObject> poolQueue = poolDic[prefab];
        GameObject obj;

        if (poolQueue.Count > 0)
        {
            obj = poolQueue.Dequeue();
        }
        else
        {
            //池子空了，实例化新物体
            obj = Instantiate(prefab);
        }

        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 归还物体回池子
    /// </summary>
    public void Release(GameObject obj, GameObject sourcePrefab)
    {
        if (obj == null) return;

        // 如果字典不存在该预制体池子，新建
        if (!poolDic.ContainsKey(sourcePrefab))
        {
            poolDic.Add(sourcePrefab, new Queue<GameObject>());
        }

        obj.SetActive(false);
        //放回池子父物体，层级不乱
        obj.transform.SetParent(poolRoot);
        poolDic[sourcePrefab].Enqueue(obj);
    }

    /// <summary>
    /// 预生成一批物体，游戏启动时预热池子
    /// </summary>
    public void WarmPool(GameObject prefab, int count)
    {
        if (!poolDic.ContainsKey(prefab))
        {
            poolDic.Add(prefab, new Queue<GameObject>());
        }
        Queue<GameObject> queue = poolDic[prefab];
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, poolRoot);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }
    }
}
