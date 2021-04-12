// <copyright file="ProjectRepository.cs">
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
    using Dev31.TodoApp.Utilities.Helpers;
    using System.Linq;

    /// <summary>
    /// Class ProjectRepository
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProjectRepository : BaseRepository, IRepository<Project, PostOptions, Guid>
    {
        /// <summary>
        /// Constructor for the Repository
        /// <see cref="ProjectRepository"/>
        /// </summary>
        /// <param name="context">AppDbContext</param>
        public ProjectRepository(AppDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Delete method, deletes an entry of Projects, and mark the task inside of it as deleted
        /// </summary>
        /// <param name="entity">Instance of a Project</param>
        public void Delete(Project entity)
        {
            foreach(var task in entity.Tasks)
            {
                task.ProjectUuid = new Guid("00000000-0000-0000-0000-000000000000");
                task.Status = "Deleted";
                task.End = Helpers.DateToIsoString();
                _context.Tasks.Update(task);
            }
            _context.Projects.Remove(entity);
        }

        /// <summary>
        /// Returns asyncronously a list with all the projects joining with tasks and tags
        /// </summary>
        /// <returns DbSet="projects">asynchronous operation</returns>
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.Include(project => project.Tasks)
                                          .ThenInclude(task => task.Tags)
                                          .ThenInclude(taskTag => taskTag.Tag)
                                          .ToListAsync();
        }

        /// <summary>
        /// Get an instance of a project from the db using the primary key to find it
        /// </summary>
        /// <param name="key">Uuid of the project to be recovered</param>
        /// <returns entity="project">Instance of the project with the uuid given</returns>
        public async Task<Project> GetByPrimaryKeyAsync(Guid key)
        {
            return await _context.Projects.Include(project => project.Tasks)
                                          .ThenInclude(task => task.Tags)
                                          .SingleOrDefaultAsync(task => task.Uuid == key);
        }

        /// <summary>
        /// Adds a new project to the database
        /// </summary>
        /// <param name="entity">Instance of a project to be added</param>
        public async Task AddAsync(Project entity)
        {
            await _context.Projects.AddAsync(entity);
        }

        /// <summary>
        /// Updates an existing Project in the database
        /// </summary>
        /// <param name="entity">Project with the attributes to be updatedt</param>
        public void Update(Project entity)
        {
            _context.Projects.Update(entity);
        }

        /// <summary>
        /// Get a list of the projects that are children of the current project
        /// </summary>
        /// <param name="key">Uuid of the project to find its children</param>
        /// <returns entity="project">List of the children projects of the project given</returns>
        public async Task<IEnumerable<Project>> GetChildren(Guid uuid)
        {
            return await _context.Projects.Where(project => project.Parent == uuid)
                                          .Include(project => project.Tasks)
                                          .ToListAsync();
        }

        public Task<IEnumerable<Project>> GetAllAsync(PostOptions options)
        {
            throw new NotImplementedException();
        }
    }
}