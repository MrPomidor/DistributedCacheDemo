using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Common.PublishSubscribe.Redis;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRedisMessaging(this IServiceCollection services)
    {
        services
            .AddTransient<IMessagePublisher, RedisMessagePublisher>();

        services.AddHostedService<RedisMessageConsumerBackgroundService>();

        return services;
    }
}
