using EZFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//全局场景控制器，负责管理所有场景的生命周期以及场景切换的逻辑
public class SceneController : MonoSingletonTemplateScript<SceneController>
{
    //当前场景的类型
    public SceneType CurScene=SceneType.ST_MENU;

    //当前游戏模式
    public GameMode CurMode { get; set; } = GameMode.Null;

    //加载的百分比
    private int LoadPercent;

    //显示的百分比
    private int DisplayPercent;


    //自定义初始化方法
    public void OnLoad()
    {

    }

    //加载新的场景（切换场景操作）
    public void LoadScene(SceneType m_SceneType,bool ShowImage)
    {
        //获取目标场景名称
        string sceneName = SceneConstant.GetNameWithType(m_SceneType);

        //判断场景是否存在
        if (!string.IsNullOrEmpty(sceneName))
            StartCoroutine(FuncLoadScene(sceneName, m_SceneType,ShowImage));
        else
            Debug.LogError("加载目标场景失败，目标场景为空");
    }

    IEnumerator FuncLoadScene(string m_SceneName, SceneType m_SceneType,bool Show)
    {
        LoadPercent = 0;
        DisplayPercent = 0;

        //显示读条界面
        UIController.Instance.ShowLoadingCanvas(true,Show);
        yield return new WaitForSeconds(2.0f);
        //创建一个异步操作用来执行场景的切换函数
        AsyncOperation tmpAsync = SceneManager.LoadSceneAsync(m_SceneName);
        //此时场景还未加载完毕，不可以激活
        tmpAsync.allowSceneActivation = false;

        //做一个循环，不断的分帧加载场景中的游戏对象，读条
        while (tmpAsync.progress<0.9f)
        {
            //计算出进度百分比的数值
            LoadPercent = (int)(tmpAsync.progress * 100);
            //让数字不断的上升到百分比的数值
            while (DisplayPercent < LoadPercent)
            {
                ++DisplayPercent;
                //同步加载进度到进度条的显示
                UIController.Instance.UpdateProgressValue(DisplayPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        //进度已加载至100%
        LoadPercent = 100;
        //让数字不断的上升到百分比的数值
        while (DisplayPercent < LoadPercent)
        {
            ++DisplayPercent;
            //同步加载进度到进度条的显示
            UIController.Instance.UpdateProgressValue(DisplayPercent);
            yield return new WaitForEndOfFrame();
        }

        //此时新场景已加载完毕，可以激活
        tmpAsync.allowSceneActivation = true;
        //将当前场景切换为新的场景
        CurScene = m_SceneType;

        //隐藏读条界面
        UIController.Instance.ShowLoadingCanvas(false,false);
    }

}
