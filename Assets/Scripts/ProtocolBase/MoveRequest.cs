using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWorkAndData
{
    public class MoveRequest : GameRoomProtocolBase
    {
        public MoveRequest(int ID)
        {
            this.type = ProtocolTypes.MoveRequest;
            id = ID;
        }
        public int id; 
    }
}
