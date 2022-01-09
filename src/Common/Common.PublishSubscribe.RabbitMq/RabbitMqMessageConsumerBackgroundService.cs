using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Common.PublishSubscribe.RabbitMq;

public class RabbitMqMessageConsumerBackgroundService : IHostedService
{
    private readonly IChannelProvider _channelProvider;
    private readonly IEnumerable<IRabbitMqMessageConsumer> _consumers;

    private IModel? _channel;

    public RabbitMqMessageConsumerBackgroundService(IChannelProvider channelProvider, IEnumerable<IRabbitMqMessageConsumer> consumers)
    {
        _channelProvider = channelProvider;
        _consumers = consumers;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _channel = _channelProvider.GetChannel();

        foreach (var consumer in _consumers)
        {
            await consumer.Register(_channel);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel == null)
            return;

        foreach (var consumer in _consumers)
        {
            await consumer.Unregister(_channel);
        }
        _channel = null;
    }
}
