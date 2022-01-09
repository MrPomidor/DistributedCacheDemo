using Common;

namespace HybridCacheApi.Redis.Controllers;

public class DataController : DataControllerBase
{
    public DataController(ICache itemCache, IDataFactory dataFactory) : base(itemCache, dataFactory)
    {
    }
}
