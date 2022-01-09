using Common;
using Common.HybridCache;
using Common.PublishSubscribe.Redis;
using StackExchange.Redis;

namespace HybridCacheApi.Redis.HybridCache;

public class CacheExpirationMessageConsumer : IRedisMessageConsumer
{
    private readonly IInstanceIdentifierProvider _instanceIdentifierProvider;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly ISerializer _serializer;
    private readonly Common.HybridCache.HybridCache _cache;

    private ISubscriber? _subscriber;
    private ChannelMessageQueue? _channel;

    public CacheExpirationMessageConsumer(IInstanceIdentifierProvider instanceIdentifierProvider, IConnectionMultiplexer connectionMultiplexer, ISerializer serializer, Common.HybridCache.HybridCache cache)
    {
        _instanceIdentifierProvider = instanceIdentifierProvider;
        _connectionMultiplexer = connectionMultiplexer;
        _serializer = serializer;
        _cache = cache;
    }

    public async Task Register()
    {
        _subscriber = _connectionMultiplexer.GetSubscriber();

        _channel = await _subscriber.SubscribeAsync(HybridCacheConsts.CacheExpirationTopicName);

        _channel.OnMessage(OnMessageReceived);
    }

    public async Task Unregister()
    {
        if (_channel != null)
        {
            await _channel.UnsubscribeAsync();
            _channel = null;
        }

        if (_subscriber != null)
        {
            await _subscriber.UnsubscribeAllAsync();
            _subscriber = null;
        }
    }

    private async Task OnMessageReceived(ChannelMessage channelMessage)
    {
        var message = _serializer.Deserialize<CacheExpiredMessage>(channelMessage.Message);
        if (message.OriginatorInstance == _instanceIdentifierProvider.GetIdentifier())
            return; // TODO logging

        await _cache.ClearInstanceMemoryAsync(message.CacheKey);
    }
}
