using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWorkAndData
{
    public class TakeOffRequest : ProtocolBase
    {
        public TakeOffRequest(int ID)
        {
            this.type = ProtocolTypes.TakeOffRequest;
            id = ID;
        }
        public int id;

    }
}

