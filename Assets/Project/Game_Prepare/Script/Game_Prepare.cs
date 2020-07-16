using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData;
/// <summary>
/// ѡ����Ϸģʽö��
/// </summary>
public enum GameModeType
{
    /// <summary>
    /// ������ϰ
    /// </summary>
    SingleGame,
    /// <summary>
    /// ���߶Կ�
    /// </summary>
    NetWorKingGame,
    /// <summary>
    /// ���Ѷ�ս
    /// </summary>
    FriendGame,
    /// <summary>
    /// ƥ��
    /// </summary>
    CreareaRoomGame,

}

public class Game_Prepare : EZMonoBehaviour
{
    private Transform thisTra;

    public RoomStatus RoomStatus;
    public GameObject CreareaRoomGame;

    #region ����
    private GameObject SingTemp;//����
    private Button OpenGame;//������Ϸ
    private Button SingleClose;//�˳���Ϸ
    #endregion


    #region ���߶Կ�
    private GameObject NetWorkingTemp;//���߶Կ�
    private Button ExpertClick;
    private Button MiddlerankClick;
    private Button CommonClick;
    private Button ElementaryClick;
    public Button NetWorkingClose;
    #endregion

    #region ���Ѷ�ս
    private GameObject FrendTemp;//���Ѷ�ս
    private Button FrendClose;//�˳���Ϸ
    #endregion

    #region ��������
    private Image ImageOne;
    private Text TextOne;
    private Image ImageTwo;
    private Text TextTwo;
    private Image ImageThree;
    private Text TextThree;
    private Image ImageFour;
    private Text TextFour;
    #endregion

    public GameModeType ContModeType;//��Ϸģʽö��

    [HideInInspector] public EnterRandomGameRoomRequest request = new EnterRandomGameRoomRequest();//������Ϸ����

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
        Debug.Log("����ģʽ�򵥴�������ģʽ��������߼�");
        SceneController.Instance.LoadScene(SceneType.ST_GAME, false);
    }

    void CloseMethod()
    {
        EZComponent.RemoveConment<Game_Prepare>();
    }
    /// <summary>
    /// �������߶Կ�
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
    /// ƥ�䷿��
    /// </summary>
    void CreareaRoomMethod()
    {
        SingTemp.SetActive(false);
        NetWorkingTemp.SetActive(false);
        FrendTemp.SetActive(false);
        CreareaRoomGame.SetActive(true);
    }

    /// <summary>
    ///ѡ�񳡴εķ���
    /// </summary>
    void OnClickMethod(GameLevelType type)
    {
        request.gameLevelType = type;
        EZComponent.AddConment<Game_SelectItem>();//����ѡ�����ҳ��
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
