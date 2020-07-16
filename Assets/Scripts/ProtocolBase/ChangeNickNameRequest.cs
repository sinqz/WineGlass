using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 请求更改昵称头像
/// </summary>
namespace NetWorkAndData
{
    [Serializable]
    public class ChangeNickNameRequest : ProtocolBase
    {
        public ChangeNickNameRequest()
        {
            this.type = ProtocolTypes.ChangeNickNameRequest;
        }
        //要修改的昵称
        public string nickName;

        //要修改的头像ID
        public string userItemId;

    }

}
