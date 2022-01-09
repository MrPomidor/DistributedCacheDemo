using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Common.PublishSubscribe.Redis;

public class RedisMessagePublisher : IMessagePublisher
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ISerializer _serializer;

    public RedisMessagePublisher(IConnectionMultiplexer redis, ISerializer serializer)
    {
        _redis = redis;
        _serializer = serializer;
    }

    public async Task Publish<TMessage>(string topic, TMessage message)
    {
        var messageStr = _serializer.Serialize(message);

        var subscriber = _redis.GetSubscriber();

        await subscriber.PublishAsync(topic, messageStr);
    }
}
