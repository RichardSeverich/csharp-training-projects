using Medium.Publications.Domain.Entities;
using Medium.Publications.Domain.Factories;
using Medium.Publications.Domain.Repositories;
using System.Collections.Generic;

namespace Medium.Publications.Services
{
    public class PublicationServices
    {
        private PublicationFactory _publicationFactory;
        private IPublicationRepository _publicationRepository;
        public PublicationServices(
            PublicationFactory publicationFactory,
            IPublicationRepository publicationRepository

            )
        {
            _publicationFactory = publicationFactory;
            _publicationRepository = publicationRepository;
        }

        public IEnumerable<Publication> GetAll()
        {
            return _publicationRepository.GetAll();
        }

        public Publication GetById(string id)
        {
            return _publicationRepository.GetById(id);
        }

        public void Create(string title, string author) {
            var publication = _publicationFactory.Create(title, author, "");
            _publicationRepository.Create(publication);
        }

        public void Update(string id, Publication publication) {
            _publicationRepository.Update(id, publication);
        }

        public void Delete(string id) {
            _publicationRepository.Delete(id);
        }

        public IEnumerable<Publication> FindByAuthor(string author) {
            return _publicationRepository.FindByAuthor(author);
        }
    }
}
