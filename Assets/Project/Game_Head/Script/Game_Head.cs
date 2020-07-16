using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData.APIS;

public class Game_Head : EZMonoBehaviour
{
    private Transform thisTra;
    private Button AddCoinBtn;//购买金币
    private Button AddDiamondBtn;//购买钻石
    private Text CoinText;//金币文本
    private Text DiamondText;//钻石文本
    private Button Setup;
    private Button Signin;
    private Button Certidication;




    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        CoinText = thisTra.Find("Home_Image_Coin/Home_Coin_Text").GetComponent<Text>();
        DiamondText = thisTra.Find("Home_Image_Diamond/Home_Diamond_Text").GetComponent<Text>();
        AddCoinBtn = thisTra.Find("Home_Image_Coin/Home_Coin_Add").GetComponent<Button>();
        AddDiamondBtn = thisTra.Find("Home_Image_Diamond/Home_Diamond_Add").GetComponent<Button>();
        Setup = thisTra.Find("Home_Setup").GetComponent<Button>();
        Signin = thisTra.Find("Home_Signin").GetComponent<Button>();
        Certidication = thisTra.Find("Home_Certification").GetComponent<Button>();

        Setup.onClick.AddListener(SetupMethod);
        Signin.onClick.AddListener(SigninMethod);
        Certidication.onClick.AddListener(CertidicationMethod);
        AddCoinBtn.onClick.AddListener(AddCoinMethod);
        AddDiamondBtn.onClick.AddListener(AddDiamondMethod);

    }

    //每帧最后同步玩家信息
    public override void LateUpdate()
    {
        //金币
        CoinText.text = "" + Utility.TransUnit(Actor.Coin);
        //钻石
        DiamondText.text = "" + Utility.TransUnit(Actor.Diamond);
    }

    //增加金币
    private void AddCoinMethod()
    {
        SafetyCutOff();
        Debug.Log("增加金币");
        if (EZComponent.GetConment<Game_ShopPing>() != null)
            EZComponent.GetConment<Game_ShopPing>().CoinToggle.isOn = true; //导航到金币
        else
        {
            if (EZComponent.GetConment<Game_Lobby>() != null)
                EZComponent.RemoveConment<Game_Lobby>();
            EZComponent.AddConment<Game_ShopPing>();
            //导航到金币
            EZComponent.GetConment<Game_ShopPing>().CoinToggle.isOn = true;
        }
    }

    //增加钻石
    private void AddDiamondMethod()
    {
        SafetyCutOff();
        Debug.Log("增加钻石");
        if (EZComponent.GetConment<Game_ShopPing>() != null)
            EZComponent.GetConment<Game_ShopPing>().DiamondToggle.isOn = true; //导航到金币
        else
        {
            if (EZComponent.GetConment<Game_Lobby>() != null)
                EZComponent.RemoveConment<Game_Lobby>();
            EZComponent.AddConment<Game_ShopPing>();
            //导航到金币
            EZComponent.GetConment<Game_ShopPing>().DiamondToggle.isOn = true;
        }
    }

    /// <summary>
    /// 安全判断，确保打开充值页面时已关闭掉除商城以外的其他页面
    /// </summary>
    void SafetyCutOff()
    {
        //安全判断
        if (EZComponent.GetConment<Game_Friend>() != null)
            EZComponent.RemoveConment<Game_Friend>();
        if (EZComponent.GetConment<Game_Ranking>() != null)
            EZComponent.RemoveConment<Game_Ranking>();
        if (EZComponent.GetConment<Game_Task>() != null)
            EZComponent.RemoveConment<Game_Task>();
        if (EZComponent.GetConment<Game_MagicBox>() != null)
            EZComponent.RemoveConment<Game_MagicBox>();
        if (EZComponent.GetConment<Game_Knapsack>() != null)
            EZComponent.RemoveConment<Game_Knapsack>();
        if (EZComponent.GetConment<Game_Abs>() != null)
            EZComponent.RemoveConment<Game_Abs>();
    }

    //实名认证
    private void CertidicationMethod()
    {
        EZComponent.AddConment<Game_RealName>();
    }
    //每日礼物
    private void SigninMethod()
    {
        EZComponent.AddConment<Game_Gift>();
    }
    //设置
    private void SetupMethod()
    {
        EZComponent.AddConment<Game_Setup>();
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
