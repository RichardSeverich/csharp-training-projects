using Medium.Posts.Domain.PostAggregate;
using System;

namespace Medium.Posts.RestAPI.Dto
{
    public class PostDTO
    {
        public string Id { get; set; }

        public string BlogId  { get; set; }

        public string PublicationId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Author Author { get; set; }

        public string Avatar { get; set; }

        public DateTime ReleaseDate { get; set; }

        public static PostDTO FromEntity(Post entity)
        {
            return new PostDTO
            {
                Id = entity.Id.ToString(),
                BlogId = entity.BlogId.ToString(),
                PublicationId = entity.PublicationId.ToString(),
                Title = entity.Title,
                Content = entity.Content,
                Author = entity.Author,
                Avatar = entity.Avatar,
                ReleaseDate = entity.ReleaseDate,
            };
        }

        public Post ToEntity()
        {
            return new Post
            {
                Id = Guid.Parse(Id),
                BlogId = Guid.Parse(BlogId),
                PublicationId = Guid.Parse(PublicationId),
                Title = Title,
                Content = Content,
                Author = Author,
                Avatar = Avatar,
                ReleaseDate = ReleaseDate,
            };
        }
    }
}
