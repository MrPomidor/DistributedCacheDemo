using Common;
using Common.HybridCache;
using Common.PublishSubscribe;
using Common.PublishSubscribe.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HybridCacheApi.RabbitMq.HybridCache;

public class CacheExpirationMessageConsumer : IRabbitMqMessageConsumer
{
    private readonly IInstanceIdentifierProvider _instanceIdentifierProvider;
    private readonly IMessageFormatter _messageFormatter;
    private readonly Common.HybridCache.HybridCache _cache;

    private AsyncEventingBasicConsumer? _consumer;
    private string? _consumerTag;

    public CacheExpirationMessageConsumer(IInstanceIdentifierProvider instanceIdentifierProvider, IMessageFormatter messageFormatter, Common.HybridCache.HybridCache cache)
    {
        _instanceIdentifierProvider = instanceIdentifierProvider;
        _messageFormatter = messageFormatter;
        _cache = cache;
    }

    public Task Register(IModel channel)
    {
        channel.ExchangeDeclare(HybridCacheConsts.CacheExpirationTopicName, ExchangeType.Fanout);

        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName,
                          exchange: HybridCacheConsts.CacheExpirationTopicName,
                          routingKey: "");

        _consumer = new AsyncEventingBasicConsumer(channel);
        _consumer.Received += OnMessageReceived;

        _consumerTag = channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: _consumer);

        return Task.CompletedTask;
    }

    public Task Unregister(IModel channel)
    {
        if (!string.IsNullOrEmpty(_consumerTag))
        {
            channel.BasicCancel(_consumerTag);
        }

        if (_consumer != null)
        {
            _consumer.Received -= OnMessageReceived;
        }

        _consumerTag = null;
        _consumer = null;

        return Task.CompletedTask;
    }

    private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        var message = _messageFormatter.GetMessage<CacheExpiredMessage>(eventArgs.Body.ToArray());
        if (message.OriginatorInstance == _instanceIdentifierProvider.GetIdentifier())
            return; // TODO logging

        await _cache.ClearInstanceMemoryAsync(message.CacheKey);
    }
}
