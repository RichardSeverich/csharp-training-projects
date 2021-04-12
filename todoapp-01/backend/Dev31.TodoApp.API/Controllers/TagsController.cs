// <copyright file="TagsController.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dev31.TodoApp.API.Resources;
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.Utilities.Extensions;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Class Controler for Tags
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class TagsController : ControllerBase
    {
        /// <summary>
        /// Constructor for TagController
        /// </summary>
        /// <see cref="TagsController"/>
        /// <param name="tagService">Instance of TagService</param>
        /// <param name="mapper">Instance of mapper</param>
        public TagsController(ITagService<TodoAppAPIResponse<Tag>> tagService, IMapper mapper)
        {
            _mapper = mapper;
            _tagService = tagService;
        }

        /// <summary>
        /// Controller, HttpGet endpoint for API. 
        /// </summary>
        /// <returns resources="TagResource">Returns a IEnumerable of TagResources</returns>
        [HttpGet]
        [EnableCors("TodoAppPolicy")]
        public async Task<IEnumerable<TagResource>> GetAllAsync()
        {
            var tags = await _tagService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);

            return resources;
        }

        /// <summary>
        /// Controller, HttpPost for the API
        /// </summary>
        /// <param name="resource">body</param>
        /// <returns result="TagResponce">Returns a Response Ok or BadRequest depending on the ModelState and the work of the repository</returns>
        [HttpPost]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> PostAsync([FromBody] SaveTagResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tag = _mapper.Map<SaveTagResource, Tag>(resource);
            var result = await _tagService.SaveAsync(tag);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Entity);
        }

        /// <summary>
        /// Controller, HttpDelete for the API
        /// </summary>
        /// <param name="name">string</param>
        /// <returns tag="TagResource">Returns a IActionResult Ok or BadResponse depneding in the success of the repository work</returns>
        [HttpDelete("{name}")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            var result = await _tagService.DeleteAsync(name);

            if (!result.Success)
                return BadRequest(result.Message);

            var tag = _mapper.Map<Tag, TagResource>(result.Entity);
            return Ok(tag);
        }

        /// <summary>
        /// property _mapper
        private IMapper _mapper;

        /// property _tagService
        /// </summary>
        private ITagService<TodoAppAPIResponse<Tag>> _tagService;
    }
}
