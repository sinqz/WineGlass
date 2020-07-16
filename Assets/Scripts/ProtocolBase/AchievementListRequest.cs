
namespace NetWorkAndData
{
    public class AchievementListRequest : ProtocolBase
    {
        public AchievementListRequest()
        {
            this.type = ProtocolTypes.AchievementListRequest;
        }
        public bool dayQuest;
    }

}
