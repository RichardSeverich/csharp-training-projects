// <copyright file="IUnitOfWork.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Interfaces.Repositories
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface to be implemented by th UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
