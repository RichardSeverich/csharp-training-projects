using System;

namespace Medium.Posts.Application.IntegrationEventHandler
{
    public class Publication
    {
        public string Id { get; set; }

        public string BlogId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string PublishedDate { get; set; }

        public PublicationContent Content { get; set; }

        public void Publish()
        {
        }
    }
}
