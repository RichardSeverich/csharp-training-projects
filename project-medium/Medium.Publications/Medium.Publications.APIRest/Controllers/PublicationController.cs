using Medium.Publications.APIRest.Dto;
using Medium.Publications.Services;
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
        private readonly PublicationServices _publicationServices;

        public PublicationController(PublicationServices publicationServices)
        {
            _publicationServices = publicationServices;
        }

        [HttpGet()]
        public IEnumerable<PublicationDTO> GetAll()
        {
            return _publicationServices.GetAll()
                .Select(p => PublicationDTO.FromEntity(p));
        }

        [HttpGet("{id}")]
        public PublicationDTO GetById(Guid id)
        {
            return PublicationDTO
                .FromEntity(_publicationServices.GetById(id.ToString()));
        }

        [HttpGet("author/{author}")]
        public IEnumerable<PublicationDTO> GetByAuthor(String author)
        {
            return _publicationServices.FindByAuthor(author)
                .Select(p => PublicationDTO.FromEntity(p));
        }

        [HttpPost()]
        public String Post([FromBody] PublicationDTO publicationDTO)
        {
            _publicationServices.Create(publicationDTO.Title, publicationDTO.Author);
            return "Created";
        }

        [HttpDelete("{id}")]
        public String Delete(Guid id)
        {
            _publicationServices.Delete(id.ToString());
            return "Deleted";
        }

        [HttpPut("{id}")]
        public String Update(Guid id, [FromBody] PublicationDTO publicationDTO)
        {
            _publicationServices.Update(id.ToString(), publicationDTO.ToEntity());
            return "Updated";
        }

    }
}
