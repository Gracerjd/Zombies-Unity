using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //控制面板透明度的组件
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed = 10;
    //是否要隐藏的标识
    private bool isShow = false;
    //隐藏面板的委托
    private UnityAction unityAction = null;
    //写成保护的方法不让外部去点出来使用，但是子类有可能用生命函数做一些处理（Awake比Start早执行）
    protected virtual void Awake()
    {
        canvasGroup= this.GetComponent<CanvasGroup>();
        if (canvasGroup == null )
            canvasGroup= gameObject.AddComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }
    /// <summary>
    /// 抽象方法，自己不实现逻辑，交给子类去重写实现
    /// </summary>
    public abstract void Init();
    
    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }
    /// <summary>
    /// 参数设为回调函数，为了实现隐藏完面板后再执行函数的功能
    /// </summary>
    /// <param name="callBack"></param>
    public virtual void HideMe(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow= false;
        this.unityAction = callBack;
        unityAction?.Invoke();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        //为了让isShow为true时会一直进入判断 才加上另一个条件避免浪费性能
        if ( isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha>=1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if (!isShow&&canvasGroup.alpha!=0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha<=0)
            {
                canvasGroup.alpha = 0;
            }
        }
    }
}
