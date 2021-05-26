using System;

namespace Medium.Posts.Domain.PostAggregate
{
    public class Post
    {
        public Guid Id { get; set; }

        public Guid PublicationId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Author Author { get; set; }

        public string Avatar { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Post() {
            ReleaseDate = DateTime.Now;
        }

    }
}
