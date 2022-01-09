using Common.HybridCache;
using Common.PublishSubscribe.RabbitMq;
using HybridCacheApi.RabbitMq.HybridCache;

namespace HybridCacheApi.RabbitMq;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqHybridCache(this IServiceCollection services, string rabbitMqConnectionString)
    {
        return services
            .AddHybridCache()
            .AddRabbitMqMessaging(rabbitMqConnectionString)
            .AddSingleton<IRabbitMqMessageConsumer, CacheExpirationMessageConsumer>();
    }
}
