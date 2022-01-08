using RabbitMQ.Client;

namespace Common.PublishSubscribe;

public interface IMessageConsumer
{
    Task Register(IModel channel);

    Task Unregister(IModel channel);
}
