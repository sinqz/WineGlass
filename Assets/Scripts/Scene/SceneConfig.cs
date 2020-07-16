using UnityEngine;

//场景配置文件，专门用来配置某一个场景需要用到的数据信息
public class SceneConfig : MonoBehaviour
{
    [Tooltip("场景的BGM路径")]
    public string BGMPath;


    private void Start()
    {
        Invoke("PlayBGM", 0.5f);
    }


    //播放场景背景音乐的方法
    void PlayBGM()
    {
        AudioController.MPlayer.PlayMusic(BGMPath);
    }
}
