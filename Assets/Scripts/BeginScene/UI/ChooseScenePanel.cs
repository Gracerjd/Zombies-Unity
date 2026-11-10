using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnBack;
    public Text txtContent;
    public Image imgScene;
    private int nowIndex;
    private SceneInfo nowsceneInfo;
    public override void Init()
    {
        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
            {
                nowIndex = GameDataMgr.Instance.sceneInfoLists.Count - 1;
            }
            ChangeScene();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex>=GameDataMgr.Instance.sceneInfoLists.Count)
            {
                nowIndex = 0;
            }
            ChangeScene();
        });
        btnStart.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<ChooseScenePanel>();
            //因为同步加载和初始化间隔太快，导致执行爆空，所以采用异步加载,再用回调函数加载完再执行
            //SceneManager.LoadScene(nowsceneInfo.sceneName);
            AsyncOperation async= SceneManager.LoadSceneAsync(nowsceneInfo.sceneName);
            async.completed += (obj) =>
            {
                GameLevelMgr.Instance.InitInfo(nowsceneInfo);
            };

        });
        btnBack.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<ChooseScenePanel>();
            UIMgr.Instance.ShowPanel<ChoosePanel>();
        });
        //一开始执行一次
        ChangeScene();
    }
    public void ChangeScene()
    {
        nowsceneInfo=GameDataMgr.Instance.sceneInfoLists[nowIndex];
        imgScene.sprite = Resources.Load<Sprite>(nowsceneInfo.imgRes);
        txtContent.text = "名称：" + nowsceneInfo.name + "\n描述：" + nowsceneInfo.tips;
    }
}
