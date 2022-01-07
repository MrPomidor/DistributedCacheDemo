using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;

namespace Common.InMemory;

public class InMemoryCache : ICache
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T Get<T>([NotNull] string key)
    {
        return _memoryCache.Get<T>(key);
    }

    public async Task<T> GetOrCreateAsync<T>([NotNull] string key, [NotNull] Func<string, Task<T>> itemFactory)
    {
        return await _memoryCache.GetOrCreateAsync(key, async (cacheEntry) =>
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(Consts.CacheAbsoluteExpirationMilliseconds);
            return await itemFactory(key);
        });
    }

    public Task ClearAsync([NotNull] string key)
    {
        _memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}
