using Medium.Posts.Application.EventBus;
using Medium.Posts.DomainServices;
using Medium.Posts.Domain.Factories;
using System;
using Medium.Posts.Domain.PostAggregate;

namespace Medium.Posts.Application.IntegrationEventHandler
{
    public class PublicationPublishedIntegrationEventHandler : IIntegrationEventHandler<PublicationPublishedIntegrationEvent>
    {
        private readonly PostsService _postService;
        private readonly PostFactory _postFactory;

        public PublicationPublishedIntegrationEventHandler(
            PostsService postService, 
            PostFactory postFactory)
        {
            _postService = postService;
            _postFactory = postFactory;
        }

        public void Handle(PublicationPublishedIntegrationEvent integrationEvent)
        {
            Post newPost = _postFactory.Create(
                integrationEvent.Publication.Id,
                integrationEvent.Publication.BlogId,
                integrationEvent.Publication.Title,
                integrationEvent.Publication.Content.Content,
                integrationEvent.Publication.Author,
                "",
                integrationEvent.Publication.PublishedDate,
                1
                );
            _postService.Create(newPost);
            Console.WriteLine("Post Created Successfully");
            // PostCreatedIntegrationEvent -> SearchContext
        }
    }
}
