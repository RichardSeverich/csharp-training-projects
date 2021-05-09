using Medium.Publications.Domain.ValueObjects;
using System;

namespace Medium.Publications.Domain.Entities
{
    public class Publication
    {
        public Guid Id {get; set;}

        public string Title { get; set; }

        public string Author { get; set; }

        public PublicationContent Content { get; set; }
    }
}
