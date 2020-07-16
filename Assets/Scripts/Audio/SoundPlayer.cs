using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音效播放器，负责播放游戏中所有效果音的播放与停止等逻辑
public class SoundPlayer : MonoBehaviour
{
    //所有音效的文件资源路径
    public string SoundPath = "Audio/Sound/";

    //音效的音量
    [Range(0, 1)]
    public float SoundVolume = 1f;

    //是否停止播放
    public bool SoundStoped;

    //缓存声音控制器
    private AudioController AController;

    //游戏中用到的所有声音音效的字典
    Dictionary<string, AudioSource> SoundDict = new Dictionary<string, AudioSource>();

    //自定义初始化的方法
    public void OnLoad(AudioController m_Controller)
    {
        AController = m_Controller;
    }

    /// <summary>
    /// 设置音效音量的方法
    /// </summary>
    /// <param name="m_Volume"></param>
    public void SetVolume(float m_Volume)
    {
        //修改当前的音量
        SoundVolume = m_Volume;
        //混入总音量的大小
        SoundVolume *= AController.AudioVolume;
        //限制音量大小在0~1之间
        SoundVolume = Mathf.Clamp(SoundVolume, 0, 1);

        //同步所有字典中的音效的音量为最终的音量
        foreach (KeyValuePair<string, AudioSource> tmpValue in SoundDict)
        {
            //如果声音不为空
            if (tmpValue.Value != null)
                tmpValue.Value.volume = SoundVolume;
        }
    }

    /// <summary>
    /// 停止所有音效的播放
    /// </summary>
    public void StopAllSound()
    {
        //同步所有字典中的音效的音量为最终的音量
        foreach (KeyValuePair<string, AudioSource> tmpValue in SoundDict)
        {
            //如果声音不为空
            if (tmpValue.Value != null)
                tmpValue.Value.Stop();
        }
        SoundStoped = true;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySound()
    {
        SoundStoped = false;
    }


    /// <summary>
    /// 播放音效函数，负责播放某一个音效
    /// </summary>
    /// <param name="m_SoundName"></param>
    public void PlaySound(string m_SoundName)
    {
        PlaySound(m_SoundName, false, 1, 500, 1);
    }

    /// <summary>
    /// 播放音效函数重载
    /// </summary>
    /// <param name="m_SoundName"></param>
    /// <param name="m_IsLoop"></param>
    /// <param name="m_MinDistance"></param>
    /// <param name="m_MaxDistance"></param>
    /// <param name="m_SpatialBlend"></param>
    public void PlaySound(string m_SoundName, bool m_IsLoop, float m_MinDistance, float m_MaxDistance, int m_SpatialBlend)
    {
        //如果当前已经停止所有音效的播放
        if (SoundStoped)
            return;
        //音效名称不为空
        if (!string.IsNullOrEmpty(m_SoundName))
        {
            //如果音效字典里已加入了这个音效
            if (SoundDict.ContainsKey(m_SoundName))
            {
                //如果将要播放的音效没有在播放中
                if (!SoundDict[m_SoundName].isPlaying)
                    SoundDict[m_SoundName].Play();
                return;
            }

            //音效字典里没有这个音效，需要第1次手动创建并加入到字典
            AudioClip tmpSound = GetAudioClip(m_SoundName);
            //音效文件是否存在
            if (tmpSound != null)
            {
                //创建一个用于播放指定音效的游戏对象
                GameObject tmpObj = new GameObject(m_SoundName);
                tmpObj.transform.parent = transform;
                tmpObj.transform.localPosition = Vector3.zero;
                //添加声音源组件
                AudioSource tmpAudio = tmpObj.AddComponent<AudioSource>();
                //设置声音切片
                tmpAudio.clip = tmpSound;
                //设置最小最大传播距离
                tmpAudio.minDistance = m_MinDistance;
                tmpAudio.maxDistance = m_MaxDistance;
                tmpAudio.spatialBlend = m_SpatialBlend;
                //设置是否循环
                tmpAudio.loop = m_IsLoop;
                tmpAudio.pitch = 1f;
                //设置音量
                tmpAudio.volume = SoundVolume;
                //开始播放
                tmpAudio.Play();
                //将当前音效添加至字典中
                SoundDict.Add(m_SoundName, tmpAudio);
            }
        }

    }

    //从Resources目录中加载指定的声音切片
    AudioClip GetAudioClip(string m_SoundName)
    {
        return Resources.Load<AudioClip>(SoundPath + m_SoundName);
    }
}
