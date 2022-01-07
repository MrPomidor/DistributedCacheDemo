using System.Diagnostics.CodeAnalysis;
using Common;
using Common.InMemory;
using Common.PublishSubscribe;
using Common.Redis;

namespace HybridCacheApi.HybridCache;

public class HybridCache : ICache
{
    private readonly IInstanceIdentifierProvider _instanceIdentifierProvider;
    private readonly InMemoryCache _inMemoryCache;
    private readonly RedisCache _redisCache;
    private readonly IMessagePublisher _messagePublisher;

    public HybridCache(IInstanceIdentifierProvider instanceIdentifierProvider, InMemoryCache inMemoryCache, RedisCache redisCache, IMessagePublisher messagePublisher)
    {
        _instanceIdentifierProvider = instanceIdentifierProvider;
        _inMemoryCache = inMemoryCache;
        _redisCache = redisCache;
        _messagePublisher = messagePublisher;
    }

    public async Task<T> GetOrCreateAsync<T>([NotNull] string key, [NotNull] Func<string, Task<T>> itemFactory)
    {
        var valueFromCache = _inMemoryCache.Get<T>(key);
        if (valueFromCache != null)
            return valueFromCache;

        valueFromCache = await _redisCache.GetOrCreateAsync(key, itemFactory);
        return valueFromCache;
    }

    public async Task ClearAsync([NotNull] string key)
    {
        await _redisCache.ClearAsync(key);

        await _inMemoryCache.ClearAsync(key);

        await _messagePublisher.Publish(HybridCacheConsts.CacheExpirationTopicName, new CacheExpiredMessage
        {
            OriginatorInstance = _instanceIdentifierProvider.GetIdentifier(),
            CacheKey = key
        });
    }

    public async Task ClearInstanceMemoryAsync([NotNull] string key)
    {
        await _inMemoryCache.ClearAsync(key);
    }
}
