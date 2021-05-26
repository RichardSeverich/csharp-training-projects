namespace Medium.Posts.Application.EventBus
{
    public interface IEventBus
    {
        void Subscribe<IE, IEH>() where IE : IIntegrationEvent where IEH : IIntegrationEventHandler<IE>;
    }
}
