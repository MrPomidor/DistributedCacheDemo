namespace Common.PublishSubscribe;

public interface IMessagePublisher
{
    Task Publish<TMessage>(string topic, TMessage message);
}
