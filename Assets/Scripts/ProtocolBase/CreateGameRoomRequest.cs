using System;
using System.Collections.Generic;

namespace NetWorkAndData
{
    [Serializable]
    public class CreateGameRoomRequest : ProtocolBase
    {
        public CreateGameRoomRequest()
        {
            this.type = ProtocolTypes.CreateGameRoomRequest;
        }
        //玩家本局选择的道具
        public List<string> readyItems;
    }
}
