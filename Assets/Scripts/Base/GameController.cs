using EZFramework;
using NetWorkAndData;
using NetWorkAndData.APIS;
using System.Collections;
using UnityEngine;
//游戏控制器类，负责管理游戏中其他的控制器的生命周期以及游戏中全局通用逻辑
public class GameController : MonoSingletonTemplateScript<GameController>
{

    //声音控制器
    private AudioController AudioControl;

    //场景控制器
    private SceneController SceneControl;

    //UI控制器
    private UIController UIControl;

    //Mob控制器
    private ShareSDKController ShareSDKControl;

    //事件控制器
    private EventController EventControl;

    //网络控制器
    private NetworkController NetworkControl;

    protected override void Awake()
    {
      
        base.Awake();
        //在整个游戏过程中永久存在
        DontDestroyOnLoad(this);
        //游戏全局初始化方法，所有全局资源和参数在此进行初始化
        InIt();
    }
    //初始化
    void InIt()
    {
        //调用其他控制器的初始化方法初始化其他控制器的资源和参数
        AudioControl = AudioController.Instance;
        AudioControl.OnLoad();

        SceneControl = SceneController.Instance;
        SceneControl.OnLoad();

        UIControl = UIController.Instance;
        UIControl.OnLoad();

        EventControl = EventController.Instance;
        EventControl.OnLoad();

        ShareSDKControl = Instantiate(Resources.Load<ShareSDKController>("Prefabs/ShareSDKController"));
        ShareSDKControl.gameObject.name = "ShareSDKController";
        ShareSDKControl.OnLoad();

        NetworkControl = NetworkController.Instance;
        NetworkControl.OnLoad();
    }



}
