
namespace NetWorkAndData
{
    public class EnterGameRoomRequest : CreateGameRoomRequest
    {
        public EnterGameRoomRequest()
        {
            this.type = ProtocolTypes.EnterGameRoomRequest;
        }
        //进入房间的ID
        public string roomId;
    }
}
