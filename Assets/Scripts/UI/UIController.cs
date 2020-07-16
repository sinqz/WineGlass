using System.Collections;
using EZFramework;

//UI管理器，负责切换时UI的显示隐藏逻辑
public class UIController : MonoSingletonTemplateScript<UIController>
{

    //自定义初始化方法
    public void OnLoad()
    {
    }

    //显示或关闭读条界面方法，在需要场景切换时调用此方法跳转到读条界面以及切换完成时销毁此界面
    public void ShowLoadingCanvas(bool m_Show, bool m_ShowImage)
    {
        if (m_Show)
        {
            EZComponent.AddConment<UILoading>();
            EZComponent.GetConment<UILoading>().ShowImage(m_ShowImage);
        }
        else
            EZComponent.RemoveConment<UILoading>();
    }

    //同步场景切换进度条数据
    public void UpdateProgressValue(int _Value)
    {
        EZComponent.GetConment<UILoading>().UpdateProgressValue(_Value);
    }
}
