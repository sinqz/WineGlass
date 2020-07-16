using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NetWorkAndData
{
    public class ChangeSceneCompleteRequest : GameRoomProtocolBase
    {
        public ChangeSceneCompleteRequest()
        {
            this.type = ProtocolTypes.ChangeSceneCompleteRequest;
        }
    }

}
