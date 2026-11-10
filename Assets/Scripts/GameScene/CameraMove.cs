using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //摄像机看向的对象
    public Transform target;
    //摄像机偏移位置
    public Vector3 offsetPos;
    //摄像机要看向的高度偏移
    public float bodyHeight;
    public float moveSpeed;
    public float roundSpeed;
    private Vector3 targetPos;
    private Quaternion targetQ;
    

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
            return;
        //位移相关
        targetPos = target.position + target.forward * offsetPos.z;
        targetPos += Vector3.up * offsetPos.y;
        targetPos += target.right * offsetPos.x;
        this.transform.position = Vector3.Lerp(this.transform.position,targetPos,moveSpeed*Time.deltaTime);
        //旋转相关
        targetQ = Quaternion.LookRotation(target.position+Vector3.up*bodyHeight-this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,targetQ,roundSpeed*Time.deltaTime);
    }
    //提供给外部设置摄像机位置
    public void SetTargetPos(Transform player)
    {
        target=player;
    }

    
    
}
