// <copyright file="TaskTagRepository.cs">
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
    using System.Threading.Tasks;

    /// <summary>
    /// Class TaskTagRepository
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TaskTagRepository : BaseRepository, IRepository<TaskTag, PostOptions, Guid>
    {
        /// <summary>
        /// Constructor for the Repository
        /// </summary>
        /// <see cref="TaskTagRepository"/>
        /// <param name="context"></param>
        public TaskTagRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="entity">Not Implemented yet</param>
        public void Delete(TaskTag entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public Task<IEnumerable<TaskTag>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public Task<TaskTag> GetByPrimaryKeyAsync(Guid uuid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new relation between tasks and tags to the database
        /// </summary>
        /// <param name="entity">Instance of the relation between Tasks and tags</param>
        public async Task AddAsync(TaskTag entity)
        {
            await _context.TaskTags.AddAsync(entity);
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public void Update(TaskTag entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public Task<IEnumerable<TaskTag>> GetChildren(Guid uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskTag>> GetAllAsync(PostOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
