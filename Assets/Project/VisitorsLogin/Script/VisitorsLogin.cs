using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using NetWorkAndData;
using NetWorkAndData.APIS;

public class VisitorsLogin : EZMonoBehaviour
{
    private Transform thisTra;

    private Button CloseBtn;

    private Button EnterBtn;

    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        CloseBtn = thisTra.Find("VisitorBGImage/Close").GetComponent<Button>();
        EnterBtn = thisTra.Find("Enter").GetComponent<Button>();

        CloseBtn.onClick.AddListener(() => { EZComponent.RemoveConment<VisitorsLogin>(); EZComponent.AddConment<MenuLogin>(); });
        EnterBtn.onClick.AddListener(VisitorLogin);
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

    /// <summary>
    /// 游客登录的方法
    /// </summary>
    private void VisitorLogin()
    {
        Debug.Log("游客登录的方法");
        SignInRequest signIn = new SignInRequest();
        if (PlayerPrefs.GetString(APIS.accountId) != "")
        {
            Debug.Log(PlayerPrefs.GetString(APIS.accountId));
            signIn.signInType = ConstSignInType.guest;
            signIn.accountId = PlayerPrefs.GetString(APIS.accountId);
            NetworkController.Instance.Send(signIn);
            return;
        }
        NetworkController.Instance.Send(signIn);
    }
}
