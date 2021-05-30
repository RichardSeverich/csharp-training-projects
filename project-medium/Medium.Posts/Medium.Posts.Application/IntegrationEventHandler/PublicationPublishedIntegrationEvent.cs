using Medium.Posts.Application.EventBus;

namespace Medium.Posts.Application.IntegrationEventHandler
{
    public class PublicationPublishedIntegrationEvent : IIntegrationEvent
    {
        public Publication Publication { get; set; }

        public PublicationPublishedIntegrationEvent(Publication publication)
        {
            Publication = publication;
        }
    }
}
