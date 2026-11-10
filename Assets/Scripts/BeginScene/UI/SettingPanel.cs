using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;
    public override void Init()
    {
        //初始化数据
        MusicData musicData = GameDataMgr.Instance.musicData;
        togMusic.isOn = musicData.isOpenMusic;
        togSound.isOn = musicData.isOpenSound;
        sliderMusic.value = musicData.musicValue;
        sliderSound.value = musicData.soundValue;
        btnClose.onClick.AddListener(() =>
        {
            //只有关闭界面才会存储数据
            GameDataMgr.Instance.SaveMusicData();

            UIMgr.Instance.HidePanel<SettingPanel>();
            
        });
        togMusic.onValueChanged.AddListener((v) =>
        {
            //开关背景音乐
            BKMusic.Instance.SetOpenMusic(v);
            //记录音乐开关的数据
            GameDataMgr.Instance.musicData.isOpenMusic = v;
        });
        togSound.onValueChanged.AddListener((v) =>
        {
            //记录音效开关的数据
            GameDataMgr.Instance.musicData.isOpenSound = v;
        });
        sliderMusic.onValueChanged.AddListener((v) =>
        {
            //调整音乐大小
            BKMusic.Instance.ChangeValue(v);
            //记录音乐数据
            GameDataMgr.Instance.musicData.musicValue = v;
        });
        sliderSound.onValueChanged.AddListener((v) =>
        {
            GameDataMgr.Instance.musicData.soundValue = v;
        });
    }
}
