using Microsoft.Extensions.Hosting;

namespace Common.PublishSubscribe.Redis;

public class RedisMessageConsumerBackgroundService : IHostedService
{
    private readonly IEnumerable<IRedisMessageConsumer> _consumers;

    public RedisMessageConsumerBackgroundService(IEnumerable<IRedisMessageConsumer> consumers)
    {
        _consumers = consumers;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var consumer in _consumers)
        {
            await consumer.Register();
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var consumer in _consumers)
        {
            await consumer.Unregister();
        }
    }
}
