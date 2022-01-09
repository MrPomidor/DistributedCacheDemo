using RabbitMQ.Client;

namespace Common.PublishSubscribe.RabbitMq;

public interface IChannelProvider
{
    IModel GetChannel();
}
