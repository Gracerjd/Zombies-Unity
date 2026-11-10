using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnAbout;
    public Button btnEnd;

    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            //播放选角动画
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                UIMgr.Instance.ShowPanel<ChoosePanel>();
            });

            //隐藏面板
            UIMgr.Instance.HidePanel<BeginPanel>();
        });
        btnSetting.onClick.AddListener(() =>
        {
            //显示设置面板
            UIMgr.Instance.ShowPanel<SettingPanel>();
            
            
        });
        btnAbout.onClick.AddListener(() =>
        {
            //显示关于面板
            //隐藏面板
        });
        btnEnd.onClick.AddListener(() =>
        {
            //退出游戏
            Application.Quit();
            
        });
    }
}
