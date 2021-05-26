using Medium.Posts.Application;
using Medium.Posts.RestAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medium.Posts.RestAPI.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly PostsApplication _app;

        public PostsController(PostsApplication app)
        {
            _app = app;
        }

        [HttpGet()]
        public IEnumerable<PostDTO> GetAll()
        {
            return _app.PostService.GetAll()
                .Select(p => PostDTO.FromEntity(p));
        }

        [HttpGet("{id}")]
        public PostDTO GetById(Guid id)
        {
            return PostDTO
                .FromEntity(_app.PostService.GetById(id.ToString()));
        }

        [HttpGet("author/{author}")]
        public IEnumerable<PostDTO> GetByAuthor(String author)
        {
            return _app.PostService.FindByAuthor(author)
                .Select(p => PostDTO.FromEntity(p));
        }

        [HttpPost()]
        public String Post([FromBody] PostDTO postDTO)
        {
            _app.PostService.Create(postDTO.ToEntity());
            return "Created";
        }

        [HttpDelete("{id}")]
        public String Delete(Guid id)
        {
            _app.PostService.Delete(id.ToString());
            return "Deleted";
        }

        [HttpPut("{id}")]
        public String Update(Guid id, [FromBody] PostDTO postDTO)
        {
            _app.PostService.Update(id.ToString(), postDTO.ToEntity());
            return "Updated";
        }
    }
}
