// <copyright file="ITaskService.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Interfaces.Services
{
    using Dev31.TodoApp.Models;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// interface
    /// </summary>
    public interface ITaskService<T>
    {
        Task<T> DeleteAsync(Guid id);
        Task<TodoTask> FindAsync(Guid id);
        Task<PagedList<TodoTask>> ListAsync(PostOptions options);
        Task<T> UpdateStatusAsync(Guid id, TodoTask task);
        Task<T> SaveAsync(TodoTask task);
        Task<T> UpdateAsync(Guid id, TodoTask task);
    }
}
