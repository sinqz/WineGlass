using UnityEngine;
using UnityEngine.UI;
using EZFramework;

public class MenuLogin : EZMonoBehaviour
{
    private Transform thisTra;

    private Button WeChatEnter;//΢�ŵ�¼

    private Button VistorsEnter;//�ο͵�¼

    private Button AppleEnter;//ƻ����¼

    private Button OtherEnter;//������ʽ��¼



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
        Debug.Log("΢�ŵ�¼");
    }

    private void VistorLogin()
    {
        Debug.Log("�ο͵�¼");
        EZComponent.AddConment<VisitorsLogin>();
        EZComponent.RemoveConment<MenuLogin>();
    }

    private void AppleLogin()
    {
        Debug.Log("ƻ����¼");
    }

    private void OtherLogin()
    {
        Debug.Log("������ʽ��¼");
        EZComponent.AddConment<OtherLogin>();
        EZComponent.RemoveConment<MenuLogin>();
    }

}
