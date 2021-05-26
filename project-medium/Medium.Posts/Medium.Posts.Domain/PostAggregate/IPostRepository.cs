using System.Collections.Generic;

namespace Medium.Posts.Domain.PostAggregate
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
