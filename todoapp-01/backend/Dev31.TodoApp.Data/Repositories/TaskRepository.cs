// <copyright file="TaskRepository.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Data.Repositories
{
    using Dev31.TodoApp.Data.Contexts;
    using Dev31.TodoApp.Interfaces.Repositories;
    using Dev31.TodoApp.Models;
    using System.Diagnostics.CodeAnalysis;
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Dev31.TodoApp.Data.Filters;
    using System.Linq;

    /// <summary>
    /// Class TaskRepository
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TaskRepository : BaseRepository, IRepository<TodoTask, PostOptions, Guid>
    {
        /// <summary>
        /// Constructor for the Repository
        /// </summary>
        /// <see cref="TagRepository"/>
        /// <param name="context">AppDbContext</param>
        public TaskRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Delete method, not implemented yet
        /// </summary>
        public void Delete(TodoTask entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TodoTask>> GetAllAsync()
        {
            return await _context.Tasks.Include(task => task.Tags)
                                       .ThenInclude(tasktag => tasktag.Tag)
                                       .Include(task => task.Project).ToListAsync();
        }

        /// <summary>
        /// Returns asyncronously a list with all the tsks joining with tasks
        /// </summary>
        /// <returns DbSet="tasks">Thread with a List of tsks including its tags and projects</returns>
        public async Task<IEnumerable<TodoTask>> GetAllAsync(PostOptions options)
        {
            var tasks = (IQueryable<TodoTask>) _context.Tasks.Include(task => task.Tags)
                                      .ThenInclude(tasktag => tasktag.Tag)
                                      .Include(task => task.Project);
            var taskFilters = new TaskFilter(options);
            tasks = taskFilters.Filter(tasks);

            return await tasks.ToListAsync();
        }

        /// <summary>
        /// Get an instance of a task from the db using the primary key to find it
        /// </summary>
        /// <param name="uuid">Uuid of the task to be recovered</param>
        /// <returns entity="task">Instance of the task with the name given</returns>
        public async Task<TodoTask> GetByPrimaryKeyAsync(Guid uuid)
        {
            return await _context.Tasks.Include(task => task.Tags).Include(task => task.Project).SingleOrDefaultAsync(t => t.Uuid == uuid);
        }

        /// <summary>
        /// Adds a new task to the database 
        /// </summary>
        /// <param name="entity">Instance of a task to be added</param>
        public async Task AddAsync(TodoTask entity)
        {
            await _context.Tasks.AddAsync(entity);
        }

        /// <summary>
        /// Updates an existing Task in the database
        /// </summary>
        /// <param name="entity">Instance of TodoTask with the attributes to be updated in the DB</param>
        public void Update(TodoTask entity)
        {
            _context.Tasks.Update(entity);
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="uuid">Not Implemented yet</param>
        public Task<IEnumerable<TodoTask>> GetChildren(Guid uuid)
        {
            throw new NotImplementedException();
        }
    }
}
