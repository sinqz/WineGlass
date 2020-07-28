using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData;
using NetWorkAndData.APIS;
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
    public Toggle CreareaRoomGame;

    #region ����
    private Toggle SingTemp;//����
    private Button OpenGame;//������Ϸ
    private Button SingleClose;//�˳���Ϸ
    #endregion

    #region ���߶Կ�
    private Toggle NetWorkingTemp;//���߶Կ�
    private Button ExpertClick;
    private Button MiddlerankClick;
    private Button CommonClick;
    private Button ElementaryClick;
    public Button NetWorkingClose;
    #endregion

    #region ���Ѷ�ս
    private Toggle FrendTemp;//���Ѷ�ս
    private Button FrendClose;//�˳���Ϸ
    #endregion

    #region ��������
    private Image PlayerImage;
    private Text PlayerName;
    #endregion

    public GameModeType ContModeType;//��Ϸģʽö��

    [HideInInspector] public EnterRandomGameRoomRequest request = new EnterRandomGameRoomRequest();//������Ϸ����

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
    /// ���뵥��ģʽ
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
    /// ����ģʽ������Ϸ����
    /// </summary>
    void OpenGameMethod()
    {
        OpenGame.gameObject.SetActive(false);//�رտ�ʼ��Ϸ��ť
        SceneController.Instance.CurMode = GameMode.Stand;
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
    /// ƥ�䷿��
    /// </summary>
    void CreareaRoomMethod()
    {
        CreareaRoomGame.isOn = true;
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
