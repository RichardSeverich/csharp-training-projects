using Medium.Publications.APIRest.Dto;
using Medium.Publications.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medium.Publications.APIRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicationController : ControllerBase
    {
        private readonly App _app;

        public PublicationController(App app)
        {
            _app = app;
        }

        [HttpGet()]
        public IEnumerable<PublicationDTO> GetAll()
        {
            return _app.publicationServices.GetAll()
                .Select(p => PublicationDTO.FromEntity(p));
        }

        [HttpGet("{id}")]
        public PublicationDTO GetById(Guid id)
        {
            return PublicationDTO
                .FromEntity(_app.publicationServices.GetById(id.ToString()));
        }

        [HttpGet("author/{author}")]
        public IEnumerable<PublicationDTO> GetByAuthor(String author)
        {
            return _app.publicationServices.FindByAuthor(author)
                .Select(p => PublicationDTO.FromEntity(p));
        }

        [HttpPost()]
        public String Post([FromBody] PublicationDTO publicationDTO)
        {
            _app.publicationServices.Create(publicationDTO.Title, publicationDTO.Author);
            return "Created";
        }

        [HttpDelete("{id}")]
        public String Delete(Guid id)
        {
            _app.publicationServices.Delete(id.ToString());
            return "Deleted";
        }

        [HttpPut("{id}")]
        public String Update(Guid id, [FromBody] PublicationDTO publicationDTO)
        {
            _app.publicationServices.Update(id.ToString(), publicationDTO.ToEntity());
            return "Updated";
        }

    }
}
