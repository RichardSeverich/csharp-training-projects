// <copyright file="TagRepository.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Data.Repositories
{
    using Dev31.TodoApp.Data.Contexts;
    using Dev31.TodoApp.Interfaces.Repositories;
    using Dev31.TodoApp.Models;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class TagRepository
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TagRepository : BaseRepository, IRepository<Tag, PostOptions, string>
    {
        /// <summary>
        /// Constructor for the Repository
        /// </summary>
        /// <see cref="TagRepository"/>
        /// <param name="context">AppDbContext</param>
        public TagRepository(AppDbContext context) : base (context)
        {

        }

        /// <summary>
        /// Delete method, deletes an entry of tag
        /// </summary>
        /// <param name="entity">Tag to be removed from the DB</param>
        public void Delete(Tag entity)
        {
            _context.Tags.Remove(entity);
        }

        /// <summary>
        /// Returns asyncronously a list with all the tags joining with tasks
        /// </summary>
        /// <returns DbSet="projects">Thread with a List of tags including its tasks</returns>
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.Include(tag => tag.Tasks).ToListAsync();
        }

        /// <summary>
        /// Get an instance of a tag from the db using the primary key to find it
        /// </summary>
        /// <param name="key">Name of the tag to be recovered</param>
        /// <returns entity="tag">Instance of the task with the name given</returns>
        public async Task<Tag> GetByPrimaryKeyAsync(string key)
        {
            return await _context.Tags.FindAsync(key);
        }

        /// <summary>
        /// Adds a new tag to the database
        /// </summary>
        /// <param name="entity">Instance of a tag to be added</param>
        public async Task AddAsync(Tag entity)
        {
            await _context.Tags.AddAsync(entity);
        }

        /// <summary>
        /// Updates an existing Tag in the database, Not implemented
        /// </summary>
        /// <param name="entity">Not Implemented yet</param>
        public void Update(Tag entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="uuid">Not Implemented yet</param>
        public Task<IEnumerable<Tag>> GetChildren(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAllAsync(PostOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
