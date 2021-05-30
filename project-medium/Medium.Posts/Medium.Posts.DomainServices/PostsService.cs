using Medium.Posts.Domain.PostAggregate;
using Medium.Posts.Domain.Repositories;
using System.Collections.Generic;

namespace Medium.Posts.DomainServices
{
    public class PostsService
    {
        private readonly IPostRepository _postRepository;

        public PostsService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public Post GetById(string id)
        {
            return _postRepository.GetById(id);
        }

        public void Create(Post post)
        {
            _postRepository.Create(post);
        }

        public void Update(string id, Post post)
        {
            _postRepository.Update(id, post);
        }

        public void Delete(string id)
        {
            _postRepository.Delete(id);
        }

        public IEnumerable<Post> FindByAuthor(string author)
        {
            return _postRepository.FindByAuthor(author);
        }

    }
}
