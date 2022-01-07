using Common;
using Common.PublishSubscribe;
using HybridCacheApi.HybridCache;

namespace HybridCacheApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHybridCache(this IServiceCollection services, string rabbitMqConnectionString)
    {
        return services
            .AddMessaging(rabbitMqConnectionString)
            .AddSingleton<HybridCacheMessageConsumer>()
            .AddSingleton<HybridCache.HybridCache>()
            .ReplaceSingletone<ICache, HybridCache.HybridCache>();
    }
}
