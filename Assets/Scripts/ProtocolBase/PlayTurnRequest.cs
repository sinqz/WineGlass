using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWorkAndData
{
    public class PlayTurnRequest : ProtocolBase
    {
        public PlayTurnRequest()
        {
            this.type = ProtocolTypes.PlayTurnRequest;
        }
    }

}
