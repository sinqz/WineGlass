using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class MenuLogin : EZMonoBehaviour
{
    private Transform thisTra;

    private Button WeChatEnter;//微信登录

    private Button VistorsEnter;//游客登录

    private Button AppleEnter;//苹果登录

    private Button OtherEnter;//其他方式登录



    public override void Start()
    {

        thisTra = EZUIGroup.Open(this);
        WeChatEnter = thisTra.Find("WeChatLogin").GetComponent<Button>();
        VistorsEnter = thisTra.Find("VisitorsLogin").GetComponent<Button>();
        AppleEnter = thisTra.Find("AppleLogin").GetComponent<Button>();
        OtherEnter = thisTra.Find("OthersLogin").GetComponent<Button>();
        WeChatEnter.onClick.AddListener(WeChatLogin);
        VistorsEnter.onClick.AddListener(VistorLogin);
        AppleEnter.onClick.AddListener(AppleLogin);
        OtherEnter.onClick.AddListener(OtherLogin);
    }

    public override void End()
    {
        EZUIGroup.Close(this);

    }


    private void WeChatLogin()
    {
        Debug.Log("微信登录");
    }

    private void VistorLogin()
    {
        Debug.Log("游客登录");
        EZComponent.AddConment<VisitorsLogin>();
        EZComponent.RemoveConment<MenuLogin>();
    }

    private void AppleLogin()
    {
        Debug.Log("苹果登录");
    }

    private void OtherLogin()
    {
        Debug.Log("其他方式登录");
        EZComponent.AddConment<OtherLogin>();
        EZComponent.RemoveConment<MenuLogin>();
    }

}
