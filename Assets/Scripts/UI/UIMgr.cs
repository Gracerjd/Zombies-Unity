using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr 
{
    private static UIMgr instance=new UIMgr();
    public static UIMgr Instance => instance;

    private Dictionary<string,BasePanel> dicPanel=new Dictionary<string,BasePanel>();
    public Transform canvasTrans;
    private UIMgr()
    {
        GameObject canvasObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvasObj.transform;
        GameObject.DontDestroyOnLoad(canvasObj);
    }

    /// <summary>
    /// 显示面板，返回一个泛型，让外部直接得到脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T ShowPanel<T>()where T : BasePanel
    {
        //获取面板的名字
        string panelName=typeof(T).Name;
        //判断当前是否已经存在面板了
        if(dicPanel.ContainsKey(panelName) )
        {
            return dicPanel[panelName] as T;
        }
        //根据得到的类名 直接实例化创建对应的预设体名
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        //设置其父对象
        panelObj.transform.SetParent(canvasTrans, false);
        //将 得到的对应面板脚本存储起来
        T panel= panelObj.GetComponent<T>();
        //把面板名和面板脚本储存在字典中
        dicPanel.Add(panelName, panel);
        //显示面板时就调用显示自己的方法
        panel.ShowMe();
        return panel;
    }
    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T">面板类名</typeparam>
    /// <param name="isFade">是否要淡入淡出面板</param>
    public void HidePanel<T>(bool isFade=true)where T: BasePanel
    {
        string panelName= typeof(T).Name;
        
        if (dicPanel.ContainsKey(panelName))
        {
            if (isFade)
            {
                dicPanel[panelName].HideMe(() =>
                {
                    
                    GameObject.Destroy(dicPanel[panelName].gameObject);
                    dicPanel.Remove(panelName);
                });
            }
            else
            {
                
                GameObject.Destroy(dicPanel[panelName].gameObject);
                dicPanel.Remove(panelName);
            }
        }
    }
    //得到面板
    public T GetPanel<T>()where T : BasePanel
    {
        string panelName=typeof (T).Name;
        if (dicPanel.ContainsKey(panelName))
            return dicPanel[panelName] as T;
        return null;
    }
}
