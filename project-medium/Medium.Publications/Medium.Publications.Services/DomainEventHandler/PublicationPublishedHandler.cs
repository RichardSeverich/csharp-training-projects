using MediatR;
using Medium.Publications.Domain.Events;
using Medium.Publications.Services.EventBus;
using Medium.Publications.Services.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Publications.Services.DomainEventHandler
{
    class PublicationPublishedHandler : INotificationHandler<PublicationPublished>
    {
        private readonly PublicationServices _publicationService;
        private readonly IEventBus _eventBus;

        public PublicationPublishedHandler(PublicationServices publicationService, IEventBus eventBus)
        {
            _publicationService = publicationService;
            _eventBus = eventBus;
        }

        public Task Handle(PublicationPublished publicationDomainEvent, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new PublicationPublishedIntegrationEvent(publicationDomainEvent.Publication));
            return Task.FromResult(Unit.Value);
        }
    }
}
