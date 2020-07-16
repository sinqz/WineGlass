using System;
namespace NetWorkAndData
{
    [Serializable]
    public class EnterRandomGameRoomRequest : CreateGameRoomRequest
    {
        public EnterRandomGameRoomRequest()
        {
            this.type = ProtocolTypes.EnterRandomGameRoomRequest;
        }

        /// <summary>
        /// 游戏级别
        /// </summary>
        public GameLevelType gameLevelType;
    }
}
