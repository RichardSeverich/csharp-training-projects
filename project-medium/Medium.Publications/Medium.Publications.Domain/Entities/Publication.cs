using Medium.Publications.Domain.ValueObjects;
using Medium.Publications.Domain.Events;
using System;

namespace Medium.Publications.Domain.Entities
{
    public class Publication : Entity
    {
        public Guid Id {get; set;}

        public Guid BlogId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }

        public PublicationContent Content { get; set; }

        public void Publish()
        {
            PublishedDate = DateTime.Now;
            AddEvent(new PublicationPublished(this, "1", PublishedDate));
        }
    }
}
