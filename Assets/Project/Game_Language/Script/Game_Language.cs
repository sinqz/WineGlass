using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Game_Language : EZMonoBehaviour
{
    private Transform thisTra;
    private Button Close;
    private Toggle SimplifiedChinese;
    private Toggle TraditionalChinese;
    private Toggle English;
    private Toggle Japanese;
    private Toggle Korea;

    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        Close = thisTra.Find("CloseBtn").GetComponent<Button>();
        SimplifiedChinese = thisTra.Find("LanguageToggle/SimplifiedChineseClose").GetComponent<Toggle>();
        TraditionalChinese = thisTra.Find("LanguageToggle/TraditionalChineseClose").GetComponent<Toggle>();
        English = thisTra.Find("LanguageToggle/EnglishClose").GetComponent<Toggle>();
        Japanese = thisTra.Find("LanguageToggle/JapaneseClose").GetComponent<Toggle>();
        Korea = thisTra.Find("LanguageToggle/KoreaClose").GetComponent<Toggle>();
        Close.onClick.AddListener(() => EZComponent.RemoveConment<Game_Language>());
        SimplifiedChinese.onValueChanged.AddListener(IsOn => { if (IsOn) Debug.Log("��������"); });
        TraditionalChinese.onValueChanged.AddListener(IsOn => { if (IsOn) Debug.Log("��������"); });
        English.onValueChanged.AddListener(IsOn => { if (IsOn) Debug.Log("Ӣ��"); });
        Japanese.onValueChanged.AddListener(IsOn => { if (IsOn) Debug.Log("����"); });
        Korea.onValueChanged.AddListener(IsOn => { if (IsOn) Debug.Log("����"); });
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
