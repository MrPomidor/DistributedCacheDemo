using Microsoft.Extensions.DependencyInjection;

namespace Common.PublishSubscribe.RabbitMq;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqMessaging(this IServiceCollection services, string rabbitMqConnectionString)
    {
        services
            .AddSingleton<IMessageFormatter, MessageFormatter>()
            .AddSingleton<IChannelProvider>(new ChannelProvider(rabbitMqConnectionString))
            .AddTransient<IMessagePublisher, RabbitMqMessagePublisher>();

        services.AddHostedService<RabbitMqMessageConsumerBackgroundService>();

        return services;
    }
}
