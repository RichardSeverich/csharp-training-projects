using Medium.Publications.Services.EventBus;
using RabbitMQ.Client;
using System;
using System.Text.Json;

namespace Medium.Publications.RabbitMQEventBus
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly RabbitMqConnectionSettings _connectionSettings;

        private readonly IConnectionFactory _connectionFactory;

        private readonly IConnection _connection;

        private const string BROKER_NAME = "medium_exchange";

        private const string ROUTING_KEY = "PublicationPublishedIntegrationEvent";

        private const string QUEUE_NAME = "publications_queue";

        public RabbitMQEventBus(RabbitMqConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
            _connectionFactory = new ConnectionFactory() {
                HostName = _connectionSettings.Host,
                UserName = _connectionSettings.UserName,
                Password = _connectionSettings.Password,
                Port = int.Parse(_connectionSettings.Port)
            };
            _connection = _connectionFactory.CreateConnection();
            // Create and binding exchange
            using var channel = CreateModel();
            channel.ExchangeDeclare(BROKER_NAME, ExchangeType.Direct);
            channel.QueueDeclare(QUEUE_NAME, false, false, false, null);
            channel.QueueBind(QUEUE_NAME, BROKER_NAME, ROUTING_KEY, null);
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
