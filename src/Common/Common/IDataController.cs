using Microsoft.AspNetCore.Mvc;

namespace Common;

public interface IDataController
{
    Task<DataItem> GetDataAsync();

    Task<IActionResult> ClearCacheAsync();
}
