using Microsoft.Extensions.DependencyInjection;

namespace Common.PublishSubscribe;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, string rabbitMqConnectionString)
    {
        services
            .AddSingleton<IMessageFormatter, MessageFormatter>()
            .AddSingleton<IChannelProvider>(new ChannelProvider(rabbitMqConnectionString))
            .AddTransient<IMessagePublisher, MessagePublisher>();

        services.AddHostedService<MessageConsumerBackgroundService>();

        return services;
    }
}
