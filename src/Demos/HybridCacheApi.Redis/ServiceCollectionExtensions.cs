using Common.HybridCache;
using Common.PublishSubscribe.Redis;
using HybridCacheApi.Redis.HybridCache;

namespace HybridCacheApi.Redis;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRedisHybridCache(this IServiceCollection services)
    {
        return services
            .AddHybridCache()
            .AddRedisMessaging()
            .AddSingleton<IRedisMessageConsumer, CacheExpirationMessageConsumer>();
    }
}
