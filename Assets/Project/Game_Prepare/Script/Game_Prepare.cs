using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData;
/// <summary>
/// 选择游戏模式枚举
/// </summary>
public enum GameModeType
{
    /// <summary>
    /// 单机练习
    /// </summary>
    SingleGame,
    /// <summary>
    /// 在线对抗
    /// </summary>
    NetWorKingGame,
    /// <summary>
    /// 好友对战
    /// </summary>
    FriendGame,
    /// <summary>
    /// 匹配
    /// </summary>
    CreareaRoomGame,

}

public class Game_Prepare : EZMonoBehaviour
{
    private Transform thisTra;

    public RoomStatus RoomStatus;
    public GameObject CreareaRoomGame;

    #region 单机
    private GameObject SingTemp;//单机
    private Button OpenGame;//进入游戏
    private Button SingleClose;//退出游戏
    #endregion


    #region 在线对抗
    private GameObject NetWorkingTemp;//在线对抗
    private Button ExpertClick;
    private Button MiddlerankClick;
    private Button CommonClick;
    private Button ElementaryClick;
    public Button NetWorkingClose;
    #endregion

    #region 好友对战
    private GameObject FrendTemp;//好友对战
    private Button FrendClose;//退出游戏
    #endregion

    #region 公共数据
    private Image ImageOne;
    private Text TextOne;
    private Image ImageTwo;
    private Text TextTwo;
    private Image ImageThree;
    private Text TextThree;
    private Image ImageFour;
    private Text TextFour;
    #endregion

    public GameModeType ContModeType;//游戏模式枚举

    [HideInInspector] public EnterRandomGameRoomRequest request = new EnterRandomGameRoomRequest();//进入游戏房间

    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        SingTemp = thisTra.Find("Single_Game").gameObject;
        NetWorkingTemp = thisTra.Find("NetWorking_Game").gameObject;
        FrendTemp = thisTra.Find("Frend_Game").gameObject;
        CreareaRoomGame = thisTra.Find("CreareaRoom_Game").gameObject;
        RoomStatus = thisTra.Find("CreareaRoom_Game/MatchingPlayerInfo").GetComponent<RoomStatus>();
    }

    public void ContModemethod(GameModeType MContType)
    {
        switch (MContType)
        {
            case GameModeType.SingleGame:
                NetWorkingTemp.SetActive(false);
                FrendTemp.SetActive(false);
                CreareaRoomGame.SetActive(false);
                SingTemp.SetActive(true);
                OpenGame = thisTra.Find("Single_Game/SingleOpenGame").GetComponent<Button>();
                ImageOne = thisTra.Find("Single_Game/MatchingPlayerInfo/PlayerOne").GetComponent<Image>();
                TextOne = thisTra.Find("Single_Game/MatchingPlayerInfo/PlayerOne").GetComponentInChildren<Text>();
                SingleClose = thisTra.Find("Single_Game/SingleClose").GetComponent<Button>();
                SingleClose.onClick.AddListener(CloseMethod);
                OpenGame.onClick.AddListener(OpenGameMethod);
                break;
            case GameModeType.NetWorKingGame:
                NetWorkingMethod();
                break;
            case GameModeType.FriendGame:
                SingTemp.SetActive(false);
                NetWorkingTemp.SetActive(false);
                CreareaRoomGame.SetActive(false);
                FrendTemp.SetActive(true);
                break;
            case GameModeType.CreareaRoomGame:
                CreareaRoomMethod();
                break;
        }
    }

    void OpenGameMethod()
    {
        OpenGame.gameObject.SetActive(false);
        Debug.Log("单机模式简单处理，测试模式还需添加逻辑");
        SceneController.Instance.LoadScene(SceneType.ST_GAME, false);
    }

    void CloseMethod()
    {
        EZComponent.RemoveConment<Game_Prepare>();
    }
    /// <summary>
    /// 进入在线对抗
    /// </summary>
    void NetWorkingMethod()
    {
        SingTemp.SetActive(false);
        FrendTemp.SetActive(false);
        CreareaRoomGame.SetActive(false);
        NetWorkingTemp.SetActive(true);
        ExpertClick = thisTra.Find("NetWorking_Game/Expert").GetComponent<Button>();
        MiddlerankClick = thisTra.Find("NetWorking_Game/Middlerank").GetComponent<Button>();
        CommonClick = thisTra.Find("NetWorking_Game/Common").GetComponent<Button>();
        ElementaryClick = thisTra.Find("NetWorking_Game/Elementary").GetComponent<Button>();
        NetWorkingClose = thisTra.Find("NetWorking_Game/NetWorkingClose").GetComponent<Button>();

        ExpertClick.onClick.AddListener(() => { OnClickMethod(GameLevelType.senior); });
        MiddlerankClick.onClick.AddListener(() => { OnClickMethod(GameLevelType.intermediate); });
        CommonClick.onClick.AddListener(() => { OnClickMethod(GameLevelType.ordinary); });
        ElementaryClick.onClick.AddListener(() => { OnClickMethod(GameLevelType.primary); });
        NetWorkingClose.onClick.AddListener(() => { EZComponent.RemoveConment<Game_Prepare>(); });

    }

    /// <summary>
    /// 匹配房间
    /// </summary>
    void CreareaRoomMethod()
    {
        SingTemp.SetActive(false);
        NetWorkingTemp.SetActive(false);
        FrendTemp.SetActive(false);
        CreareaRoomGame.SetActive(true);
    }

    /// <summary>
    ///选择场次的方法
    /// </summary>
    void OnClickMethod(GameLevelType type)
    {
        request.gameLevelType = type;
        EZComponent.AddConment<Game_SelectItem>();//进入选择道具页面
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
