using Medium.Posts.Domain.PostAggregate;
using System;


namespace Medium.Posts.Domain.Factories
{
    public class PostFactory
    {
        public Post Create(
            string publicationId,
            string blogId,
            string title,
            string content,
            string authorUsername,
            string avatar,
            string ReleaseDate,
            int claps
            )
        {

            var author = new Author
            {
                Id = Guid.NewGuid(),
                Username = authorUsername,
                FullName = "Default Full Name",
                Email = "Default Email"
            };

            var post = new Post
            {
                Id = Guid.NewGuid(),
                PublicationId = Guid.Parse(publicationId),
                BlogId = Guid.Parse(blogId),
                Title = title,
                Content = content,
                Author = author,
                Avatar = avatar,
                ReleaseDate = Convert.ToDateTime(ReleaseDate),
                Claps = claps
            };
            return post;
        }
    }
}
