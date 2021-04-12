// <copyright file="BaseRepository.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Data.Repositories
{
    using System.Diagnostics.CodeAnalysis;
    using Dev31.TodoApp.Data.Contexts;

    /// <summary>
    /// Class BaseRepository
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository
    {
        /// <summary>
        /// Contructor for the repository
        /// </summary>
        /// <see cref="BaseRepository"/>
        /// <param name="context">Instance of AppDbContext</param>
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// property _context Instance of AppDbContext
        /// </summary>
        protected readonly AppDbContext _context;
    }
}
