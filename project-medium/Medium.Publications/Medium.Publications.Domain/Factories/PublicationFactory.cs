using Medium.Publications.Domain.Entities;
using Medium.Publications.Domain.Events;
using Medium.Publications.Domain.ValueObjects;
using System;


namespace Medium.Publications.Domain.Factories
{
    public class PublicationFactory
    {
        public Publication Create(String title, String author, String content)
        {
            var publicationContent = new PublicationContent 
            {
                Version = 1,
                Content = content
            };
            var publication = new Publication
            {
                Id = Guid.NewGuid(),
                Title = title,
                Author = author,
                Content = publicationContent
            };
            var publishedEvent = new PublicationPublished(publication, "1", DateTime.Now);
            return publication;
        }
    }
}
