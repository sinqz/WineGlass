using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData.APIS;

public class Game_HomePage : EZMonoBehaviour
{
    private Transform thisTra;
    private Button Close;
    private Text Name;
    private Text UserID;

    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        Close = thisTra.Find("Home_Page_Close").GetComponent<Button>(); ;
        Name = thisTra.Find("Hone_HomePage/KnapsackBGImage/Mall_Data/Head_Image_Name/Name").GetComponent<Text>();
        UserID = thisTra.Find("Hone_HomePage/KnapsackBGImage/Mall_Data/Head_Image_Name/ID").GetComponent<Text>();
        Close.onClick.AddListener(CloseMethod);
    }

    public override void LateUpdate()
    {
        Name.text = Actor.nickName;
        UserID.text = Actor.userId;
    }


    void CloseMethod()
    {
        EZComponent.AddConment<Game_Lobby>();
        EZComponent.RemoveConment<Game_HomePage>();
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
