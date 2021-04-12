// <copyright file="IRepository.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Interfaces.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface to be implemented by the repositories
    /// </summary>
    public interface IRepository<TEntity, TSource, T> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(TSource options);
        Task<TEntity> GetByPrimaryKeyAsync(T key);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetChildren(T uuid);
    }

}
