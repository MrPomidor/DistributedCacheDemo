using Microsoft.Extensions.DependencyInjection;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Common.Redis;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDistributedCache(this IServiceCollection services, string redisConnectionString)
    {
        return services
            .AddRedis(redisConnectionString)
            .AddSingleton<ISerializer, NewtonsoftSerializer>()
            .ReplaceTransient<ICache, RedisCache>();
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, string redisConnectionString)
    {
        var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);

        var redLockFactory = RedLockFactory.Create(new List<RedLockMultiplexer>() { connectionMultiplexer });
        services.AddSingleton<IDistributedLockFactory>(redLockFactory);

        return services;
    }
}
