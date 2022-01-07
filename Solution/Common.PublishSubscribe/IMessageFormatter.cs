namespace Common.PublishSubscribe;

public interface IMessageFormatter
{
    byte[] GetBytes<TMessage>(TMessage message);

    TMessage GetMessage<TMessage>(byte[] bytes);
}
