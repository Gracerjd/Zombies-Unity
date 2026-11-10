using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;

    private AudioSource bkSound;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        bkSound = GetComponent<AudioSource>();
        MusicData musicData = GameDataMgr.Instance.musicData;
        SetOpenMusic(musicData.isOpenMusic);
        ChangeValue(musicData.musicValue);
    }

    public void SetOpenMusic(bool isOpen)
    {
        bkSound.mute = !isOpen;
    }
    public void ChangeValue(float value)
    {
        bkSound.volume = value;
    }
}
