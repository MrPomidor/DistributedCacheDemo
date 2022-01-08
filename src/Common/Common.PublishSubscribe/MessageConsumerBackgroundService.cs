using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Common.PublishSubscribe;

public class MessageConsumerBackgroundService : IHostedService
{
    private readonly IChannelProvider _channelProvider;
    private readonly IEnumerable<IMessageConsumer> _consumers;

    private IModel? _channel;

    public MessageConsumerBackgroundService(IChannelProvider channelProvider, IEnumerable<IMessageConsumer> consumers)
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
