using cn.sharesdk.unity3d;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetWorkAndData;

public class ShareSDKController : MonoSingletonTemplateScript<ShareSDKController>
{
    private ShareSDK mShareSDK;

    public void OnLoad()
    {
        mShareSDK = GetComponent<ShareSDK>();
        if (mShareSDK != null)
        {
            mShareSDK.shareHandler = OnShareResultHandler;
            mShareSDK.showUserHandler = OnGetUserInfoResultHandler;
            mShareSDK.authHandler = OnAuthResultHandler;
        }
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 授权的回调
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="result"></param>
    private void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("authorize success !");
        }
        else if (state == ResponseState.Fail)
        {
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

    /// <summary>
    /// 获取用户信息的回调
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    private void OnGetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get user info result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

    /// <summary>
    /// 分享的回调
    /// </summary>
    /// <param name="reqID"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    private void OnShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("share result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }


}
