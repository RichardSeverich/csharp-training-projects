// <copyright file="UnitOfWork.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Data.Repositories
{
    using System.Diagnostics.CodeAnalysis;
    using Dev31.TodoApp.Data.Contexts;
    using Dev31.TodoApp.Interfaces.Repositories;
    using System.Threading.Tasks;

    /// <summary>
    /// Class UnitOfWork
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Constructor od the UnitOfWork Class
        /// </summary>
        /// <see cref="UnitOfWork"/>
        /// <param name="context">Instance of AppDbContext</param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously saves the changes in the Database
        /// </summary>
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// property Instance of AppDbContext
        /// </summary>
        private readonly AppDbContext _context;
    }
}
