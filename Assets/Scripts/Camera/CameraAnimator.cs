using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{
    private Animator animator;
    private UnityAction overAction;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    //左转
    public void TurnLeft(UnityAction unityAction)
    {
        animator.SetTrigger("Left");
        overAction = unityAction;
    }
    //右转
    public void TurnRight(UnityAction unityAction)
    {
        animator.SetTrigger("Right");
        overAction = unityAction;
    }
    //动画结束后 调用的事件方法
    public void PlayOver()
    {
        overAction?.Invoke();
        overAction = null;
    }
}
