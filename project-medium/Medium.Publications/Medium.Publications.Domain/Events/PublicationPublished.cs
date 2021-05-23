using Medium.Publications.Domain.Entities;
using System;

namespace Medium.Publications.Domain.Events
{
    public record PublicationPublished(Publication Publication, string OwnerId, DateTime publishedDate) : IDomainEvent;
}
