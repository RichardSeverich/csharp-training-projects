using Medium.Posts.Application.EventBus;
using Medium.Posts.DomainServices;

namespace Medium.Posts.Application.IntegrationEventHandler
{
    public class OptionalPublicationPublishedIntegrationEventHandler : IIntegrationEventHandler<PublicationPublishedIntegrationEvent>
    {
        private readonly PostsService _postService;

        public OptionalPublicationPublishedIntegrationEventHandler(PostsService postService)
        {
            _postService = postService;
        }

        public void Handle(PublicationPublishedIntegrationEvent integrationEvent)
        {
            // Create a New Post
            // PostCreatedIntegrationEvent -> SearchContext
        }
    }
}
