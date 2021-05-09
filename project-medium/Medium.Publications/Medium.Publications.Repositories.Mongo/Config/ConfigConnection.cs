using Medium.Publications.Repositories.Mongo.Documents;
using MongoDB.Driver;

namespace Medium.Publications.Repositories.Mongo.Config
{
    public class ConfigConnection
    {
        private readonly ConfigSettings _settings;
        private readonly MongoClient _mongoClient;
        private const string DOCUMENT_PUBLICATION = "publications";

        public ConfigConnection(ConfigSettings settings)
        {
            _settings = settings;
            _mongoClient = new MongoClient(_settings.ConnectionString);
        }

        public IMongoCollection<PublicationDocument> Publications
        {
            get
            {
                return _mongoClient
                    .GetDatabase(_settings.DatabaseName)
                    .GetCollection<PublicationDocument>(DOCUMENT_PUBLICATION);
            }
        }
    }
}
