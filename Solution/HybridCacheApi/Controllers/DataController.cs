using Common;

namespace HybridCacheApi.Controllers;

public class DataController : DataControllerBase
{
    public DataController(ICache itemCache, IDataFactory dataFactory) : base(itemCache, dataFactory)
    {
    }
}