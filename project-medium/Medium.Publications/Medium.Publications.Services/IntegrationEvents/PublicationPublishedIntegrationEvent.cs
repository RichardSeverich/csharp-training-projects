using Medium.Publications.Domain.Entities;
using Medium.Publications.Services.EventBus;

namespace Medium.Publications.Services.IntegrationEvents
{
    public record PublicationPublishedIntegrationEvent(Publication Publication) : IIntegrationEvent;
}
