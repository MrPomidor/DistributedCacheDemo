using System.Text;

namespace Common.PublishSubscribe;

public class MessageFormatter : IMessageFormatter
{
    private readonly ISerializer _serializer;

    public MessageFormatter(ISerializer serializer)
    {
        _serializer = serializer;
    }

    public byte[] GetBytes<TMessage>(TMessage message)
    {
        var messageStr = _serializer.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(messageStr);
        return messageBytes;
    }

    public TMessage GetMessage<TMessage>(byte[] bytes)
    {
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        if (bytes.Length == 0)
            throw new ArgumentException("Message is empty", paramName: nameof(bytes));

        try
        {
            var strUtf8 = Encoding.UTF8.GetString(bytes);
            return _serializer.Deserialize<TMessage>(strUtf8);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while deserializing message", ex);
        }
    }
}
