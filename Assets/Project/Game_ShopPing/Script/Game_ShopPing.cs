using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class Game_ShopPing : EZMonoBehaviour
{
    private Transform thisTra;

    private Button Close;
    public Toggle CoinToggle { get; set; }
    public Toggle DiamondToggle { get; set; }

    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        Close = thisTra.Find("Hone_ShopPing/Home_ShopPing_Close").GetComponent<Button>();
        CoinToggle = thisTra.Find("Hone_ShopPing/Mall_Coin_ImageClose").GetComponent<Toggle>();
        DiamondToggle = thisTra.Find("Hone_ShopPing/Mall_Diamond_ImageClose").GetComponent<Toggle>();
        Close.onClick.AddListener(CloseMethod);
    }

    //退出商城的方法
    private void CloseMethod()
    {
        EZComponent.RemoveConment<Game_ShopPing>();
        EZComponent.AddConment<Game_Lobby>();
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
