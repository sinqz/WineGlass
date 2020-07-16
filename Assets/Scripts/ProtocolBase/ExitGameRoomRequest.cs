using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWorkAndData
{
    public class ExitGameRoomRequest : GameRoomProtocolBase
    {
        public ExitGameRoomRequest()
        {
            this.type = ProtocolTypes.ExitGameRoomRequest;
        }

    }


}
