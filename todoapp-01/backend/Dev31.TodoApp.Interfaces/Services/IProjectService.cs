// <copyright file="IProjectService.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Interfaces.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dev31.TodoApp.Models;

    /// <summary>
    /// interface
    /// </summary>
    public interface IProjectService<T>
    {
        Task<T> DeleteAsync(Guid id);

        Task<Project> FindAsync(Guid id);

        Task<IEnumerable<Project>> ListAsync();

        Task<T> SaveAsync(Project project);
        
        Task<T> UpdateAsync(Guid id, Project project);
    }
}
