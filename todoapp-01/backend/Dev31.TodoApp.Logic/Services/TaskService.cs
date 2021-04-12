// <copyright file="TaskService.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Logic.Services
{
    using Dev31.TodoApp.Interfaces.Repositories;
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.Utilities.Helpers;

    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Class TaskService implemet ITaskService
    /// </summary>
    public class TaskService : ITaskService<TodoAppAPIResponse<TodoTask>>
    {
        /// <summary>
        /// Constructor of the Service
        /// </summary>
        /// <param name="taskRepository">Instance of a TaskRepository</param>
        /// <param name="unitOfWork">Instance of UnitOfWork</param>
        public TaskService(IRepository<TodoTask, PostOptions, Guid> taskRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Changes the status od a existing task to deleted whitout actually deleting it
        /// </summary>
        /// <param name="id">Unique identifier of the task</param>
        /// <returns status="update status">Response depending the success of the operation</returns>
        public async Task<TodoAppAPIResponse<TodoTask>> DeleteAsync(Guid id)
        {
            var task = new TodoTask() { Status = "Deleted" };
            return await UpdateStatusAsync(id, task);
        }

        /// <summary>
        /// Find a specific task given its uuid
        /// </summary>
        /// <param name="id">Unique identifier of the task</param>
        /// <returns _taskRepository>Instance of TodoTask which uuid matches the one given</returns>
        public async Task<TodoTask> FindAsync(Guid id)
        {
            return await _taskRepository.GetByPrimaryKeyAsync(id);
        }

        /// <summary>
        /// Gets a list of all the existing tasks in the database
        /// </summary>
        /// <returns_taskRepository>List with all the existing tasks in the db</returns>
        public async Task<PagedList<TodoTask>> ListAsync(PostOptions options)
        {
            switch (options.Entry)
            {
                case "any":
                    options.Entry = null;
                    break;
                case "hour":
                    options.Entry = DateTime.Now.AddMinutes(-60).ToString("yyyy-MM-ddTHH:mm:ssZ");
                    break;
                case "day":
                    options.Entry = DateTime.Now.AddHours(-24).ToString("yyyy-MM-ddTHH:mm:ssZ");
                    break;
                case "week":
                    options.Entry = DateTime.Now.AddDays(-7).ToString("yyyy-MM-ddTHH:mm:ssZ");
                    break;
                case "month":
                    options.Entry = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-ddTHH:mm:ssZ");
                    break;
                case "year":
                    options.Entry = DateTime.Now.AddYears(-1).ToString("yyyy-MM-ddTHH:mm:ssZ");
                    break;
                default:
                    options.Entry = null;
                    break;
            }
            var tasks = await _taskRepository.GetAllAsync(options);
            return PagedList<TodoTask>.Create(tasks, options.PageNumber, options.PageSize);
        }

        /// <summary>
        /// Add a new task to the database
        /// </summary>
        /// <param name="task">Instance of TodoTask to be added</param>
        /// <returns TaskResponse="response">Response depending the success of the operation</returns>
        public async Task<TodoAppAPIResponse<TodoTask>> SaveAsync(TodoTask task)
        {
            task.Uuid = Guid.NewGuid();
            task.Entry = Helpers.DateToIsoString();
            try
            {
                await _taskRepository.AddAsync(task);
                await _unitOfWork.CompleteAsync();

                return new TodoAppAPIResponse<TodoTask>(task);
            }
            catch(Exception ex)
            {
                return new TodoAppAPIResponse<TodoTask>($"An error ocurred when saving the task: {ex.Message}");
            }

        }

        /// <summary>
        /// Modify an existing task
        /// </summary>
        /// <param name="id">Unique identifier of the task</param>
        /// <param name="task">Instance of TodoTask with the attributes to be updated</param>
        /// <returns TaskResponse="response">Response depending the success of the operation</returns>
        public async Task<TodoAppAPIResponse<TodoTask>> UpdateAsync(Guid id, TodoTask task)
        {
            var existingTask = await _taskRepository.GetByPrimaryKeyAsync(id);
            if (existingTask == null)
                return new TodoAppAPIResponse<TodoTask>("Task not found.");

            existingTask.Description = task.Description ?? existingTask.Description;
            existingTask.Priority = (task.Priority == "High" || task.Priority == "Medium" || task.Priority == "Low") ?
                task.Priority :
                existingTask.Priority;
            existingTask.Due = task.Due ?? existingTask.Due;
            existingTask.ProjectUuid = task.ProjectUuid;
            existingTask.Tags = task.Tags;
            try
            {
                _taskRepository.Update(existingTask);
                await _unitOfWork.CompleteAsync();

                return new TodoAppAPIResponse<TodoTask>(existingTask);
            }
            catch (Exception ex)
            {
                return new TodoAppAPIResponse<TodoTask>($"An error occurred when updating the task: {ex.Message}");
            }

        }

        /// <summary>
        /// Updates the status of a existing task.
        /// </summary>
        /// <param name="id">Unique identifier of the task</param>
        /// <param name="task">Instance of TodoTask with the status to be updated</param>
        /// <returns TaskResponse="response">Response depending the success of the operation</returns>
        public async Task<TodoAppAPIResponse<TodoTask>> UpdateStatusAsync(Guid id, TodoTask task)
        {
            var existingTask = await _taskRepository.GetByPrimaryKeyAsync(id);
            if (existingTask == null)
                return new TodoAppAPIResponse<TodoTask>("Task not found.");
            switch (task.Status)
            {
                case "Pending":
                    existingTask.Entry = Helpers.DateToIsoString();
                    break;
                case "In Progress":
                    existingTask.Start = Helpers.DateToIsoString();
                    break;
                case "Completed":
                case "Deleted":
                    existingTask.End = Helpers.DateToIsoString();
                    break;
                default:
                    return new TodoAppAPIResponse<TodoTask>("Status unknown");
            }
            existingTask.Status = task.Status;

            try
            {
                _taskRepository.Update(existingTask);
                await _unitOfWork.CompleteAsync();
                return new TodoAppAPIResponse<TodoTask>(existingTask);
            }
            catch(Exception ex)
            {
                return new TodoAppAPIResponse<TodoTask>($"An Error occurred when updating the status of the task: {ex.InnerException.Message}");
            }
        }

        /// <summary>
        /// property _taskRepository Instance of TaskRepository
        /// </summary>
        private IRepository<TodoTask, PostOptions, Guid> _taskRepository;

        /// <summary>
        /// property _unitOfWork Instance of UnitOfWork
        /// </summary>
        private IUnitOfWork _unitOfWork;
    }
}
