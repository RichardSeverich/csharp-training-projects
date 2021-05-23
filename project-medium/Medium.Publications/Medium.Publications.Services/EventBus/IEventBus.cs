namespace Medium.Publications.Services.EventBus
{
    public interface IEventBus
    {
        public void Publish(IIntegrationEvent @event);
    }
}
