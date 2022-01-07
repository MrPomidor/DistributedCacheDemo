using RabbitMQ.Client;

namespace Common.PublishSubscribe;

public interface IChannelProvider
{
    IModel GetChannel();
}
