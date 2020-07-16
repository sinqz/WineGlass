using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    //所有BGM音乐文件的资源路径
    private string MusicPath = "Audio/Music/";

    //BGM的音量
    [Range(0, 1)]
    public float MusicVolume = 1f;

    //当前声音源
    public AudioSource CurMusic;

    //缓存声音控制器
    private AudioController AController;

    //自定义初始化的方法
    public void OnLoad(AudioController m_Controller)
    {
        AController = m_Controller;
    }

    /// <summary>
    /// 设置修改音乐音量的方法
    /// </summary>
    /// <param name="_Volume"></param>
    public void SetVolume(float _Volume)
    {
        //修改当前BGM音量
        MusicVolume = _Volume;
        //混入总音量的大小
        MusicVolume *= AController.AudioVolume;
        //限制音量大小在0~1之间
        MusicVolume = Mathf.Clamp(MusicVolume, 0, 1);

        //同步最终的音量给声音源
        if (CurMusic != null)
            CurMusic.volume = MusicVolume;
    }

    /// <summary>
    /// 停止音乐的播放
    /// </summary>
    public void StopMusic()
    {
        //判断当前场景下是否有背景音乐
        if (CurMusic != null && CurMusic.gameObject.activeInHierarchy)
        {
            CurMusic.Stop();//停止播放
            Destroy(CurMusic.gameObject);//销毁节点
            CurMusic = null;//重置为空
        }

    }
    /// <summary>
    /// 暂停音乐播放
    /// </summary>
    public void PauseMusic()
    {
        //判断当前场景下是否有背景音乐
        if (CurMusic != null && CurMusic.gameObject.activeInHierarchy)
        {
            CurMusic.Pause();//暂停音乐播放
        }
    }

    public void PlayMusic()
    {
        //判断当前场景下是否有背景音乐
        if (CurMusic != null && CurMusic.gameObject.activeInHierarchy)
        {
            CurMusic.Play();//打开音乐播放
        }
    }

    /// <summary>
    /// 播放音乐函数，负责播放一段BGM
    /// </summary>
    /// <param name="m_MusicName"></param>
    public void PlayMusic(string m_MusicName)
    {
        PlayMusic(m_MusicName, true, 1, 500, 0);
    }

    /// <summary>
    /// 播放音乐函数重载
    /// </summary>
    /// <param name="m_MusicName"></param>
    /// <param name="m_IsLoop"></param>
    /// <param name="m_MinDistance"></param>
    /// <param name="m_MaxDistance"></param>
    /// <param name="m_SpatialBlend"></param>
    public void PlayMusic(string m_MusicName, bool m_IsLoop, float m_MinDistance, float m_MaxDistance, int m_SpatialBlend)
    {
        //首先停止当期播放的音乐
        StopMusic();
        //判断当前音乐名称不为空
        if (!string.IsNullOrEmpty(m_MusicName))
        {
            //从Resources目录中加载将要播放的声音切片
            AudioClip tmpMusic = GetAudioClip(m_MusicName);
            //如果存在
            if (tmpMusic!=null)
            {
                //创建一个用于播放BGM的游戏对象
                GameObject tmpObj = new GameObject(m_MusicName);
                tmpObj.transform.parent = transform;
                tmpObj.transform.localPosition = Vector3.zero;
                //添加声音源组件
                CurMusic = tmpObj.AddComponent<AudioSource>();
                //设置声音切片
                CurMusic.clip = tmpMusic;
                //设置最小和最大传播距离
                CurMusic.minDistance = m_MinDistance;
                CurMusic.maxDistance = m_MaxDistance;
                CurMusic.spatialBlend = m_SpatialBlend;
                //设置是否循环播放
                CurMusic.loop = m_IsLoop;
                CurMusic.pitch = 1f;
                //设置音量大小
                CurMusic.volume = MusicVolume;
                //开始播放
                CurMusic.Play();
            }
        }
    }

    //从resources目录中加载指定的声音切片
    AudioClip GetAudioClip(string m_MusicName)
    {
        return Resources.Load<AudioClip>(MusicPath + m_MusicName);
    }
}
