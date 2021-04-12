namespace Dev31.TodoApp.Interfaces.Repositories
{
    using System.Threading.Tasks;
    using Dev31.TodoApp.Models;

    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<User> GetByEmail(string email);

        Task<User> GetByUsername(string username);

        User GetById(int id);
    }
}
