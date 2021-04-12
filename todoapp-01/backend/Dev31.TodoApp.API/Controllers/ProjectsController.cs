// <copyright file="ProjectsController.cs">
//    Copyright (c) jala.
// </copyright> 
namespace Dev31.TodoApp.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dev31.TodoApp.API.Resources;
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.Utilities.Extensions;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Class controller for projects
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Constructor for ProjectsController
        /// </summary>
        /// <see cref="ProjectsController"/>
        /// <param name="projectService">Instance of ProjectService</param>
        /// <param name="mapper">Instance of IMapper</param>
        public ProjectsController(IProjectService<TodoAppAPIResponse<Project>> projectService, IMapper mapper)
        {
            _mapper = mapper;
            _projectService = projectService;
        }

        /// <summary>
        /// Controller, HttpGet Endpoint for the API
        /// </summary>
        /// <returns resources="ProjectResource">Returns a IEnumerable of the available projects in the DataBase</returns>
        [HttpGet]
        [EnableCors("TodoAppPolicy")]
        public async Task<IEnumerable<ProjectResource>> GetAllAsync()
        {
            var projects = await _projectService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);

            return resources;
        }

        /// <summary>
        /// Controller, HttpGet Ednpoint for the API
        /// </summary>
        /// <param name="id">Uuid of the project to recover</param>
        /// <returns resources="ProjectResource">Resturn a instance of project resource</returns>
        [HttpGet("{id}")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var project = await _projectService.FindAsync(id);

            if (project == null)
                return NotFound();

            var resource = _mapper.Map<Project, ProjectResource>(project);

            return Ok(resource);
        }

        /// <summary>
        /// Controller, HttpPost Endpoint for the API
        /// </summary>
        /// <param name="resource">body</param>
        /// <returns ObjectResult="BadRequest">BadRequest if the ModelState is Invalid or if it receives a message from the Service</returns>
        /// <returns ObjectResult="ok">Ok Response when everything goes right while saving and the body passed is valid</returns>
        [HttpPost]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> PostAsync([FromBody] SaveProjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var project = _mapper.Map<SaveProjectResource, Project>(resource);
            var result = await _projectService.SaveAsync(project);

            if (!result.Success)
                return BadRequest(result.Message);

            var projectResource = _mapper.Map<Project, ProjectResource>(result.Entity);

            return Ok(projectResource);
        }

        /// <summary>
        /// Controller, HttpDelete Endpoint for the API
        /// </summary>
        /// <param name="id">Uuid of the Project to be deleted</param>
        /// <returns ObjectResult="BadRequest">Bad Request in case of the response given from the service is negative</returns>
        /// <returns ObjectResult="ok">Ok response in case everything goes right during the deleting operation</returns>
        [HttpDelete("{id}")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _projectService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var projectResource = _mapper.Map<Project, ProjectResource>(result.Entity);
            
            return Ok(projectResource);
        }

        /// <summary>
        /// Controller, HttpPut Endpoint for the API
        /// </summary>
        /// <param name="id">Uuid of the Project to be updated</param>
        /// <param name="resource">Instance of Project Resource received from  the request body</param>
        /// <returns ObjectResult="BadRequest">Bad Request in case the model is onvalid or the response received from the Service is negative</returns>
        /// <returns ObjectResult="ok">Ok response in case everything goes right during the updating operation</returns>
        [HttpPut("{id}")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SaveProjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var project = _mapper.Map<SaveProjectResource, Project>(resource);
            var result = await _projectService.UpdateAsync(id, project);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Entity);
        }

        /// <summary>
        /// property _mapper
        private IMapper _mapper;

        /// property _projectService
        /// </summary>cd .
        private IProjectService<TodoAppAPIResponse<Project>> _projectService;
    }
}
