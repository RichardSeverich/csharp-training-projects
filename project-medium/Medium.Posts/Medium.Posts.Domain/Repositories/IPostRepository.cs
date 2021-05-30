using System.Collections.Generic;
using Medium.Posts.Domain.PostAggregate;

namespace Medium.Posts.Domain.Repositories
{
    public interface IPostRepository
    {
        public IEnumerable<Post> GetAll();

        public Post GetById(string id);

        public void Create(Post publication);

        public void Update(string id, Post publication);

        public void Delete(string id);

        public IEnumerable<Post> FindByAuthor(string author);
    }
}
