using Medium.Publications.Domain.Entities;
using Medium.Publications.Domain.Repositories;
using Medium.Publications.Repositories.Mongo.Config;
using Medium.Publications.Repositories.Mongo.Documents;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Medium.Publications.Repositories.Mongo
{

    public class PublicationMongoRepository : IPublicationRepository
    {

        private readonly ConfigConnection _mongo;

        public PublicationMongoRepository(ConfigConnection mongo)
        {
            _mongo = mongo;
        }

        public IEnumerable<Publication> GetAll()
        {
           return _mongo.Publications
                .Find(p => true).ToList().Select(p => p.ToEntity());
        }

        public Publication GetById(string id)
        {
            return _mongo.Publications
                .Find<PublicationDocument>(p => p.Id == id).FirstOrDefault().ToEntity();
        }

        public void Create(Publication publication)
        {
             _mongo.Publications.InsertOne(PublicationDocument.FromEntity(publication));
        }
        public void Update(string id, Publication publication)
        {
            _mongo.Publications.ReplaceOne(p => p.Id == id, PublicationDocument.FromEntity(publication));
        }
        public void Delete(string id)
        {
            _mongo.Publications.DeleteOne(p => p.Id == id);
        }

        public IEnumerable<Publication> FindByAuthor(string author)
        {
            return _mongo.Publications.Find<PublicationDocument>(p => p.Author.Equals(author))
                .ToList().Select(p => p.ToEntity());
        }

    }
}
