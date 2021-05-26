using System;
using System.Text;
using System.Text.Json;
using Autofac;
using Medium.Posts.Application.EventBus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Medium.Posts.RabbitMQEventBus
{
    public class RabbitMQEventBus : IEventBus
    {

        private readonly RabbitMqConnectionSettings _connectionSettings;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConnection _connection;

        private IModel _consumerChannel;

        private readonly ISubscriptionManager _subscriptionManager;
        private readonly ILifetimeScope _diContainer;

        private const string BROKER_NAME = "medium_exchange";

        private const string ROUTING_KEY = "PublicationPublishedIntegrationEvent";

        private const string QUEUE_NAME = "publications_queue";

        public RabbitMQEventBus(
            RabbitMqConnectionSettings connectionSettings, 
            ISubscriptionManager subscriptionManager, 
            ILifetimeScope diContainer)
        {
            _connectionSettings = connectionSettings;
            _connectionFactory = new ConnectionFactory { 
                HostName = _connectionSettings.Host,
                UserName = _connectionSettings.UserName,
                Password = _connectionSettings.Password,
                Port = int.Parse(_connectionSettings.Port)
            };
            _connection = _connectionFactory.CreateConnection();
            _consumerChannel = CreateConsumerChannel();

            /*_consumerChannel.ExchangeDeclare(BROKER_NAME, ExchangeType.Direct);
            _consumerChannel.QueueDeclare(QUEUE_NAME, false, false, false, null);
            _consumerChannel.QueueBind(QUEUE_NAME, BROKER_NAME, ROUTING_KEY, null);
            _consumerChannel.QueueDeclare(QUEUE_NAME, true);*/
            _subscriptionManager = subscriptionManager;
            _diContainer = diContainer;
        }

        private IModel CreateConsumerChannel()
        {
            return _connection.CreateModel();
        }

        public void Subscribe<IE, IEH>()
            where IE : IIntegrationEvent
            where IEH : IIntegrationEventHandler<IE>
        {
            var eventName = typeof(IE).Name;

            if (!_subscriptionManager.HasEvent(eventName)) 
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueBind(QUEUE_NAME, BROKER_NAME, eventName);
                }
            }

            _subscriptionManager.RegisterHandler(typeof(IE), typeof(IEH));

            var consumer = new EventingBasicConsumer(_consumerChannel);

            consumer.Received += OnMessageRecived;

            _consumerChannel.BasicConsume(QUEUE_NAME, false, consumer);
        }

        private void OnMessageRecived(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var rawMessage = Encoding.UTF8.GetString(e.Body.Span);

            var eventType = _subscriptionManager.GetEventByName(eventName);
            var eventHandler = _subscriptionManager.GetHandlerByEventName(eventName);

            var integrationEvent = JsonSerializer.Deserialize(rawMessage, eventType) as IIntegrationEvent;

            foreach (var handler in eventHandler)
            {
                using (var scope = _diContainer.BeginLifetimeScope())
                { 
                    var handlerInstance = _diContainer.ResolveOptional(handler);

                    if (handlerInstance == null) continue;

                    handler.GetMethod("Handle").Invoke(handlerInstance, new [] { integrationEvent });
                }
            }

            _consumerChannel.BasicAck(e.DeliveryTag, false);
        }
    }
}
