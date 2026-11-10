using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TigPanel : BasePanel
{
    public Text txtContent;
    public Button btnSure;
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<TigPanel>();
        });
    }
    public void ChangeInfo(string info)
    {
        txtContent.text= info;
    }
}
