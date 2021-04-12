namespace Dev31.TodoApp.Interfaces.Services
{
    using System.Threading.Tasks;
    using Dev31.TodoApp.Models;
    public interface IUserService<T,E>
    {
        Task<T> SaveAsync(User user);

        Task<bool> VerifyUserFields(string field, string value);

        Task<E> Authenticate(SignIn signInUser);

        User GetById(int idUser);
    }
}
