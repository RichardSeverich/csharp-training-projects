// <copyright file="TasksController.cs">
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
    using System.Linq;
    using Dev31.TodoApp.API.Resources;
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.Utilities.Extensions;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Class controller for tasks
    /// </summary>
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// Constructor for the tasks controller
        /// </summary>
        /// <see cref="ProjectsController"/>
        /// <param name="taskService">Instance of TaskService</param>
        /// <param name="mapper">Instance of IMapper</param>
        public TasksController(ITaskService<TodoAppAPIResponse<TodoTask>> taskService, IMapper mapper)
        {
            _mapper = mapper;
            _taskService = taskService;
        }

        /// <summary>
        /// Controller, HttpPost for the API
        /// </summary>
        /// <returns resources="TasksResource">IActionResult with tasks resource as response</returns>
        [HttpPost]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> GetAllAsync([FromBody] PostOptions options)
        {
            var tasks = await _taskService.ListAsync(options);
            var resources = _mapper.Map<IEnumerable<TodoTask>, IEnumerable<TaskResource>>(tasks);
            var response = new TasksResource
            {
                Data = resources,
                CurrentPage = tasks.CurrentPage,
                PageSize = tasks.PageSize,
                NextPageNumber = tasks.NextPageNumber,
                PreviousPageNumber = tasks.PreviousPageNumber,
                TotalCount = tasks.TotalCount,
                TotalPages = tasks.TotalPages
            };
            return Ok(response);
        }

        /// <summary>
        /// Controller, HttpGet for the API for getting only a specific task
        /// </summary>
        /// <param name="id">Uuid of the task to be recovered</param>
        /// <returns resource="TaskResource">Ok or BadResponse depending if the task exist in the DataBase</returns>
        [HttpGet("{id}")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var task = await _taskService.FindAsync(id);
            if (task == null)
                return BadRequest("Task not found");
            var resource = _mapper.Map<TodoTask, TaskResource>(task);
            return Ok(resource);
        }

        /// <summary>
        /// Controller, HttpPatch for the API, updates the status of a specific task
        /// </summary>
        /// <param name="id">Uuid of the task to be patched</param>
        /// <param name="task">New status received on the body of the request</param>
        /// <returns resource="TaskResource">Ok or BadResponse depending if the task exist in the DataBase</returns>
        [HttpPatch("{id}")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> UpdateStatusAsync(Guid id, [FromBody] TodoTask task)
        {
            var result = await _taskService.UpdateStatusAsync(id, task);
            if (!result.Success)
                return BadRequest(result.Message);
            var resource = _mapper.Map<TodoTask, TaskResource>(result.Entity);
            return Ok(resource);
        }

        /// <summary>
        /// Controller, HttpDelete for deleting a specific task
        /// </summary>
        /// <param name="id">Uuid of the task to be deleted</param>
        /// <returns resource="TaskResource">Ok or BadResponse depending if the task exist in the DataBase</returns>
        [HttpDelete("{id}")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _taskService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var resource = _mapper.Map<TodoTask, TaskResource>(result.Entity);
            return Ok(resource);
        }

        /// <summary>
        /// Controller, HttpPost for create a task
        /// </summary>
        /// <param name="resource">Instance of a save resource task with the parameters to be created, received from the body</param>
        /// <returns resource="TaskResponse">Ok or BadRequest depending if the ModelState is valid or if the response received from the service is negative</returns>
        [HttpPost("new-task")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> PostAsync([FromBody] SaveTaskResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var task = _mapper.Map<SaveTaskResource, TodoTask>(resource);
            var result = await _taskService.SaveAsync(task);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Entity);
        }

        /// <summary>
        /// Controller, HttpPut for update a task
        /// </summary>
        /// <param name="id">uuid of the task to be updated</param>
        /// <param name="resource">Attributes to be updated, received from the body</param>
        /// <returns taskResource="TaskResource">Ok or BadRequest depending if the ModelState is valid or if the response received from the service is negative</returns>
        [HttpPut("{id}")]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] ModifyTaskResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var task = _mapper.Map<ModifyTaskResource, TodoTask>(resource);
            var result = await _taskService.UpdateAsync(id, task);

            if (!result.Success)
                return BadRequest(result.Message);

            var taskResource = _mapper.Map<TodoTask, TaskResource>(result.Entity);
            return Ok(taskResource);
        }

        /// <summary>
        /// property _mapper
        private IMapper _mapper;

        /// property _taskService
        /// </summary>
        private ITaskService<TodoAppAPIResponse<TodoTask>> _taskService;
    }
}
