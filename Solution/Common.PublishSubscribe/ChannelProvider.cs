using RabbitMQ.Client;

namespace Common.PublishSubscribe;

public class ChannelProvider : IChannelProvider, IDisposable
{
    private IConnection? _connection;
    private IModel? _model;

    private readonly string _connectionString;

    public ChannelProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IModel GetChannel()
    {
        if (_model != null)
            return _model;

        lock (this)
        {
            if (_model != null)
                return _model;

            var connectionFactory = new ConnectionFactory { HostName = _connectionString };
            connectionFactory.DispatchConsumersAsync = true;

            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            return _model;
        }
    }

    public void Dispose()
    {
        try
        {
            _model?.Dispose();
        }
        catch { }
        try
        {
            _connection?.Dispose();
        }
        catch { }
    }
}
