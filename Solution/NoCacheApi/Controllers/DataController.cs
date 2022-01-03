using Common;

namespace NoCacheApi.Controllers;

public class DataController : DataControllerBase
{
    public DataController(ICache itemCache, IDataFactory dataFactory) : base(itemCache, dataFactory)
    {
    }
}
