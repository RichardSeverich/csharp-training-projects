using Medium.Publications.Domain.Entities;
using Medium.Publications.Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Medium.Publications.Repositories.Mongo.Documents
{
    public class PublicationDocument
    {
        [BsonId]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        PublicationContent Content { get; set; }

        public static PublicationDocument FromEntity(Publication entity)
        {
            return new PublicationDocument
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
