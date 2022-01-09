using RabbitMQ.Client;

namespace Common.PublishSubscribe.RabbitMq;

public interface IRabbitMqMessageConsumer
{
    Task Register(IModel channel);

    Task Unregister(IModel channel);
}
