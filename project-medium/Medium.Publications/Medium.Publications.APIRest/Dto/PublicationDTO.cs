using Medium.Publications.Domain.Entities;
using Medium.Publications.Domain.ValueObjects;
using System;

namespace Medium.Publications.APIRest.Dto
{
    public class PublicationDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        PublicationContent Content { get; set; }

        public static PublicationDTO FromEntity(Publication entity)
        {
            return new PublicationDTO
            {
                Id = entity.Id.ToString(),
                Title = entity.Title,
                Author = entity.Author,
                Content = entity.Content
            };
        }

        public Publication ToEntity()
        {
            return new Publication
            {
                Id = Guid.Parse(Id),
                Title = Title,
                Author = Author,
                Content = Content
            };
        }
    }
}
