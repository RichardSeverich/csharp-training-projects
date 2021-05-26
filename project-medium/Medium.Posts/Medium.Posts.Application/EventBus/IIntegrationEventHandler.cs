namespace Medium.Posts.Application.EventBus
{
    public interface IIntegrationEventHandler<IE> where IE : IIntegrationEvent
    {
        void Handle(IE integrationEvent);
    }
}
