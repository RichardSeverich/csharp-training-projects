using Medium.Posts.Domain.PostAggregate;
using Medium.Publications.Repositories.Mongo.Config;
using Medium.Publications.Repositories.Mongo.Documents;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Medium.Publications.Repositories.Mongo
{

    public class PostMongoRepository : IPostRepository
    {

        private readonly ConfigConnection _mongo;

        public PostMongoRepository(ConfigConnection mongo)
        {
            _mongo = mongo;
        }

        public IEnumerable<Post> GetAll()
        {
           return _mongo.Posts
                .Find(p => true).ToList().Select(p => p.ToEntity());
        }

        public Post GetById(string id)
        {
            return _mongo.Posts
                .Find<PostDocument>(p => p.Id == id).FirstOrDefault().ToEntity();
        }

        public void Create(Post publication)
        {
             _mongo.Posts.InsertOne(PostDocument.FromEntity(publication));
        }
        public void Update(string id, Post publication)
        {
            _mongo.Posts.ReplaceOne(p => p.Id == id, PostDocument.FromEntity(publication));
        }
        public void Delete(string id)
        {
            _mongo.Posts.DeleteOne(p => p.Id == id);
        }

        public IEnumerable<Post> FindByAuthor(string author)
        {
            return _mongo.Posts.Find<PostDocument>(p => p.Author.Equals(author))
                .ToList().Select(p => p.ToEntity());
        }

    }
}
