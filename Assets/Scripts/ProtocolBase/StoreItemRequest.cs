
namespace NetWorkAndData
{
    public class StoreItemRequest : ProtocolBase
    {
        public StoreItemRequest()
        {
            this.type = ProtocolTypes.StoreItemRequest;
        }
        public CategoryType categoryType;
    }
}
