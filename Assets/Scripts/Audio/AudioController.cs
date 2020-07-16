using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//声音管理器
public class AudioController :MonoSingletonTemplateScript<AudioController>
{

    //音乐播放器
    public static MusicPlayer MPlayer = null;

    //音效播放器
    public static SoundPlayer SPlayer = null;

    //总音量的大小
    [Range(0, 1)]
    public float AudioVolume = 1f;


   protected override void Awake()
    {
        base.Awake();
        GameObject MusicPlayer = new GameObject("MusicPlayer");
        MusicPlayer.AddComponent<MusicPlayer>();
        MusicPlayer.transform.SetParent(transform);

        GameObject SoundPlayer = new GameObject("SoundPlayer");
        SoundPlayer.AddComponent<SoundPlayer>();
        SoundPlayer.transform.SetParent(transform);
    }

    //自定义初始化的方法
    public void OnLoad()
    {
        if (MPlayer == null)
        {
            MPlayer = GetComponentInChildren<MusicPlayer>();
            MPlayer.OnLoad(this);
        }

        if (SPlayer == null)
        {
            SPlayer = GetComponentInChildren<SoundPlayer>();
            SPlayer.OnLoad(this);
        }
    }
    /// <summary>
    /// 音乐开关
    /// </summary>
    /// <param name="IsNo"></param>
    public void MusicController(bool IsOn)
    {
        if (IsOn)
            MPlayer.PauseMusic();
        else
            MPlayer.PlayMusic();
    }
    /// <summary>
    /// 音效开关
    /// </summary>
    /// <param name="IsNo"></param>
    public void SoundController(bool IsOn)
    {
        if (IsOn)
            SPlayer.StopAllSound();
        else
            SPlayer.PlaySound();
    }

}
