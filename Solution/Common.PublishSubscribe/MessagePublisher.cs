﻿using RabbitMQ.Client;

namespace Common.PublishSubscribe;

public class MessagePublisher : IMessagePublisher
{
    private readonly IMessageFormatter _messageFormatter;
    private readonly IChannelProvider _channelProvider;
    public MessagePublisher(IMessageFormatter messageFormatter, IChannelProvider channelProvider)
    {
        _messageFormatter = messageFormatter;
        _channelProvider = channelProvider;
    }

    public Task Publish<TMessage>(string topic, TMessage message)
    {
        var messageBytes = _messageFormatter.GetBytes(message);

        var channel = _channelProvider.GetChannel();

        channel.ExchangeDeclare(topic, ExchangeType.Fanout);

        channel.BasicPublish(topic, routingKey: "", basicProperties: null, body: messageBytes);

        return Task.CompletedTask;
    }
}
