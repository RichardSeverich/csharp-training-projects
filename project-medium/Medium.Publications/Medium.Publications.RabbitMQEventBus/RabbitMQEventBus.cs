using Medium.Publications.Services.EventBus;
using RabbitMQ.Client;
using System.Text.Json;

namespace Medium.Publications.RabbitMQEventBus
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly RabbitMqConnectionSettings _connectionSettings;

        private readonly IConnectionFactory _connectionFactory;

        private readonly IConnection _connection;

        private const string BROKER_NAME = "events";

        public RabbitMQEventBus(RabbitMqConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
            _connectionFactory = new ConnectionFactory() { HostName = _connectionSettings.Host };
            _connection = _connectionFactory.CreateConnection();
        }

        private IModel CreateModel()
        {
            return _connection.CreateModel();
        }

        public void Publish(IIntegrationEvent @event)
        {
            using (var channel = CreateModel())
            {

                string routingKey = @event.GetType().Name;

                byte[] body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions());

                channel.BasicPublish(BROKER_NAME, routingKey, true, channel.CreateBasicProperties(), body);
            }
        }
    }
}
