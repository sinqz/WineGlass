using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData;
using NetWorkAndData.APIS;
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
    public Toggle CreareaRoomGame;

    #region 单机
    private Toggle SingTemp;//单机
    private Button OpenGame;//进入游戏
    private Button SingleClose;//退出游戏
    #endregion

    #region 在线对抗
    private Toggle NetWorkingTemp;//在线对抗
    private Button ExpertClick;
    private Button MiddlerankClick;
    private Button CommonClick;
    private Button ElementaryClick;
    public Button NetWorkingClose;
    #endregion

    #region 好友对战
    private Toggle FrendTemp;//好友对战
    private Button FrendClose;//退出游戏
    #endregion

    #region 公共数据
    private Image PlayerImage;
    private Text PlayerName;
    #endregion

    public GameModeType ContModeType;//游戏模式枚举

    [HideInInspector] public EnterRandomGameRoomRequest request = new EnterRandomGameRoomRequest();//进入游戏房间

    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        SingTemp = thisTra.Find("Single_Game").GetComponent<Toggle>();
        NetWorkingTemp = thisTra.Find("NetWorking_Game").GetComponent<Toggle>();
        FrendTemp = thisTra.Find("Frend_Game").GetComponent<Toggle>();
        CreareaRoomGame = thisTra.Find("CreareaRoom_Game").GetComponent<Toggle>();
        RoomStatus = thisTra.Find("CreareaRoom_Game/MatchingPlayerInfo").GetComponent<RoomStatus>();
    }

    public void ContModemethod(GameModeType MContType)
    {
        switch (MContType)
        {
            case GameModeType.SingleGame:
                SingleGameMethod();
                break;
            case GameModeType.NetWorKingGame:
                NetWorkingMethod();
                break;
            case GameModeType.FriendGame:
                FrendTemp.isOn = true;
                break;
            case GameModeType.CreareaRoomGame:
                CreareaRoomMethod();
                break;
        }
    }

    /// <summary>
    /// 进入单机模式
    /// </summary>
    void SingleGameMethod()
    {
        SingTemp.isOn = true;
        OpenGame = thisTra.Find("Single_Game/SingleOpenGame").GetComponent<Button>();
        PlayerImage = thisTra.Find("Single_Game/MatchingPlayerInfo/Player").GetComponent<Image>();
        //PlayerImage.sprite = Actor.mSprite;
        PlayerName = PlayerImage.GetComponentInChildren<Text>();
        PlayerName.text = Actor.nickName;
        SingleClose = thisTra.Find("Single_Game/SingleClose").GetComponent<Button>();
        SingleClose.onClick.AddListener(CloseMethod);
        OpenGame.onClick.AddListener(OpenGameMethod);
    }

    /// <summary>
    /// 单机模式进入游戏方法
    /// </summary>
    void OpenGameMethod()
    {
        OpenGame.gameObject.SetActive(false);//关闭开始游戏按钮
        SceneController.Instance.CurMode = GameMode.Stand;
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
        NetWorkingTemp.isOn = true;
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
        CreareaRoomGame.isOn = true;
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
