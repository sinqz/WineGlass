using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData.APIS;

public class Game_Head : EZMonoBehaviour
{
    private Transform thisTra;
    private Button AddCoinBtn;//������
    private Button AddDiamondBtn;//������ʯ
    private Text CoinText;//����ı�
    private Text DiamondText;//��ʯ�ı�
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

    //ÿ֡���ͬ�������Ϣ
    public override void LateUpdate()
    {
        //���
        CoinText.text = "" + Utility.TransUnit(Actor.Coin);
        //��ʯ
        DiamondText.text = "" + Utility.TransUnit(Actor.Diamond);
    }

    //���ӽ��
    private void AddCoinMethod()
    {
        SafetyCutOff();
        Debug.Log("���ӽ��");
        if (EZComponent.GetConment<Game_ShopPing>() != null)
            EZComponent.GetConment<Game_ShopPing>().CoinToggle.isOn = true; //���������
        else
        {
            if (EZComponent.GetConment<Game_Lobby>() != null)
                EZComponent.RemoveConment<Game_Lobby>();
            EZComponent.AddConment<Game_ShopPing>();
            //���������
            EZComponent.GetConment<Game_ShopPing>().CoinToggle.isOn = true;
        }
    }

    //������ʯ
    private void AddDiamondMethod()
    {
        SafetyCutOff();
        Debug.Log("������ʯ");
        if (EZComponent.GetConment<Game_ShopPing>() != null)
            EZComponent.GetConment<Game_ShopPing>().DiamondToggle.isOn = true; //���������
        else
        {
            if (EZComponent.GetConment<Game_Lobby>() != null)
                EZComponent.RemoveConment<Game_Lobby>();
            EZComponent.AddConment<Game_ShopPing>();
            //���������
            EZComponent.GetConment<Game_ShopPing>().DiamondToggle.isOn = true;
        }
    }

    /// <summary>
    /// ��ȫ�жϣ�ȷ���򿪳�ֵҳ��ʱ�ѹرյ����̳����������ҳ��
    /// </summary>
    void SafetyCutOff()
    {
        //��ȫ�ж�
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

    //ʵ����֤
    private void CertidicationMethod()
    {
        EZComponent.AddConment<Game_RealName>();
    }
    //ÿ������
    private void SigninMethod()
    {
        EZComponent.AddConment<Game_Gift>();
    }
    //����
    private void SetupMethod()
    {
        EZComponent.AddConment<Game_Setup>();
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
