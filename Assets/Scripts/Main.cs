using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    //private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        UIMgr.Instance.ShowPanel<BeginPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        ////从主相机发射射线，鼠标点击时，检测地面（指定 Layer），打印点击到的世界坐标。
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hitInfo, 100, 1 << LayerMask.NameToLayer("Ground")))
        //    {
        //        Debug.Log(hitInfo.point);
        //        //point打印的是射线碰撞的物体表面的世界坐标，而transform.position是物体轴心的世界坐标
        //    }
        //}
        
    }
}
