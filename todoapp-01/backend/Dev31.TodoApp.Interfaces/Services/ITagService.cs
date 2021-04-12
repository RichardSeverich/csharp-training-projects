// <copyright file="ITagService.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Interfaces.Services
{
    using Dev31.TodoApp.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// interface
    /// </summary>
    public interface ITagService<T>
    {
        Task<T> DeleteAsync(string name);

        Task<IEnumerable<Tag>> ListAsync();

        Task<T> SaveAsync(Tag tag);
    }
}
