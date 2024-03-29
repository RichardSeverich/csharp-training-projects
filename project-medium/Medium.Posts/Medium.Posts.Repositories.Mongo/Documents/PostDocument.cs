﻿using Medium.Posts.Domain.PostAggregate;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Medium.Publications.Repositories.Mongo.Documents
{
    public class PostDocument
    {
        [BsonId]
        public string Id { get; set; }

        public string PublicationId { get; set; }

        public string BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Avatar { get; set; }

        public Author Author { get; set; }

        public DateTime ReleaseDate { get; set; }


        public static PostDocument FromEntity(Post entity)
        {
            return new PostDocument
            {
                Id = entity.Id.ToString(),
                PublicationId = entity.PublicationId.ToString(),
                BlogId = entity.BlogId.ToString(),
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
                PublicationId = Guid.Parse(PublicationId),
                BlogId = Guid.Parse(BlogId),
                Title = Title,
                Content = Content,
                Author = Author,
                Avatar = Avatar,
                ReleaseDate = ReleaseDate
            };
        }

    }
}
