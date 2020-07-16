using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using UnityEngine.SceneManagement;
using NetWorkAndData.APIS;

public class Game_Setup : EZMonoBehaviour
{
    private Transform thisTra;
    private Button Close;//返回
    private Toggle SoundBtn;
    private Toggle MusicBtn;
    private Button LanguageBtn;
    private Button Home_Setup_QuitBtn;
    private Text Version_Number;
    private Text UserID;

    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        Close = thisTra.Find("Home_Setup_BGImage/CloseBtn").GetComponent<Button>();
        SoundBtn = thisTra.Find("Home_Setup_SoundBtn").GetComponent<Toggle>();
        MusicBtn = thisTra.Find("Home_Setup_MusicBtn").GetComponent<Toggle>();
        LanguageBtn = thisTra.Find("LanguageBtn").GetComponent<Button>();
        Version_Number = thisTra.Find("Home_Version/Version_Number").GetComponent<Text>();
        UserID = thisTra.Find("User/ID").GetComponent<Text>();
        Home_Setup_QuitBtn = thisTra.Find("Home_Setup_QuitBtn").GetComponent<Button>();
        LanguageBtn.onClick.AddListener(LanguageMethod);
        Close.onClick.AddListener(CloseMethod);
        Home_Setup_QuitBtn.onClick.AddListener(Home_SetUpMethod);
        SoundBtn.onValueChanged.AddListener(IsOn => { AudioController.Instance.SoundController(IsOn); });
        MusicBtn.onValueChanged.AddListener(IsOn => { AudioController.Instance.MusicController(IsOn); });
    }

    public override void LateUpdate()
    {
        UserID.text = Actor.userId;//ID
        Version_Number.text = "V" + Application.version;
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

    private void LanguageMethod()
    {
        EZComponent.AddConment<Game_Language>();
    }


    private void CloseMethod()
    {
        EZComponent.RemoveConment<Game_Setup>();
    }

    private void Home_SetUpMethod()
    {
        Debug.Log("回到注册登录场景");
    }
}
