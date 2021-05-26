using Medium.Posts.DomainServices;

namespace Medium.Posts.Application
{
    public class PostsApplication
    {
        private readonly PostsService _postsService;

        public PostsApplication(PostsService postsService)
        {
            _postsService = postsService;
        }

        public PostsService PostService => _postsService;
    }
}
