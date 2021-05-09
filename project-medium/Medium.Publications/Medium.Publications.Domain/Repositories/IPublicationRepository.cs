using Medium.Publications.Domain.Entities;
using System.Collections.Generic;

namespace Medium.Publications.Domain.Repositories
{
    public interface IPublicationRepository
    {
        public IEnumerable<Publication> GetAll();

        public Publication GetById(string id);

        public void Create(Publication publication);

        public void Update(string id, Publication publication);

        public void Delete(string id);

        public IEnumerable<Publication>  FindByAuthor (string author);

    }
}
