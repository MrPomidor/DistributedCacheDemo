using Microsoft.AspNetCore.Mvc;

namespace Common;

[ApiController]
[Route("api/v1/data")]
public abstract class DataControllerBase : ControllerBase, IDataController
{
    private readonly ICache _itemCache;
    private readonly IDataFactory _dataFactory;

    protected DataControllerBase(
        ICache itemCache,
        IDataFactory dataFactory)
    {
        _itemCache = itemCache;
        _dataFactory = dataFactory;
    }

    [HttpGet("")]
    public async Task<DataItem> GetDataAsync()
    {
        return await _itemCache.GetOrCreateAsync(Consts.DateNowCacheKey, async (key) => await _dataFactory.GetDataItemAsync());
    }

    [HttpPost("")]
    public async Task<IActionResult> ClearCacheAsync()
    {
        await _itemCache.ClearAsync(Consts.DateNowCacheKey);
        return Ok();
    }

    [HttpGet("large")]
    public async Task<LargeDataItem> GetLargeDataAsync()
    {
        return await _itemCache.GetOrCreateAsync(Consts.LargeDataObjectCacheKey, async (key) => await _dataFactory.GetLargeDataItemAsync());
    }

    [HttpPost("large")]
    public async Task<IActionResult> ClearLargeCacheAsync()
    {
        await _itemCache.ClearAsync(Consts.LargeDataObjectCacheKey);
        return Ok();
    }
}
