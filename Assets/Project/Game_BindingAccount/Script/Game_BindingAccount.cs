using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using System;

public class Game_BindingAccount : EZMonoBehaviour
{
    private Transform thisTra;
    private Button Close;
    private Button WeChat;
    private Button QQ;
    private Button FaceBook;
    public override void Start()
    {
        thisTra = EZUIGroup.Open(this);
        Close = thisTra.Find("Close").GetComponent<Button>();
        WeChat = thisTra.Find("WeChat").GetComponent<Button>();
        QQ = thisTra.Find("QQ").GetComponent<Button>();
        FaceBook = thisTra.Find("FaceBook").GetComponent<Button>();
        Close.onClick.AddListener(()=> EZComponent.RemoveConment<Game_BindingAccount>());
        WeChat.onClick.AddListener(WeChatMethod);
        QQ.onClick.AddListener(QQMethod);
        FaceBook.onClick.AddListener(FaceBookMethod);
    }

    private void FaceBookMethod()
    {
        Debug.Log("°ó¶¨FaceBook");
    }

    private void QQMethod()
    {
        Debug.Log("ban");
    }

    private void WeChatMethod()
    {
    }

    public override void End()
    {
        EZUIGroup.Close(this);
    }

}
