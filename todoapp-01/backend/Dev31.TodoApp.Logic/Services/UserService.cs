namespace Dev31.TodoApp.Logic.Services
{
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Interfaces.Repositories;
    using Dev31.TodoApp.Interfaces.Services;
    using System.Threading.Tasks;

    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.Utilities.Helpers;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using Dev31.TodoApp.Utilities;
    using System.Text;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;

    public class UserService : IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>>
    {
        private IUnitOfWork _unitOfWork;
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<TodoAppAPIResponse<User>> SaveAsync(User user)
        {
            user.CreatedAt = Helpers.DateToIsoString();
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                user = await _userRepository.GetByUsername(user.Username);

                return new TodoAppAPIResponse<User>(user);
            }
            catch (Exception ex)
            {
                return new TodoAppAPIResponse<User>($"An error ocurred when save the user: {ex.Message}");
            }
        }

        public async Task<bool> VerifyUserFields(string field, string value)
        {
            User user = null;
            switch (field)
            {
                case "email":
                    user = await _userRepository.GetByEmail(value);
                    break;
                case "username":
                    user = await _userRepository.GetByUsername(value);
                    break;
            }

            if (user == null)
                return true;
            else
                return false;
        }

        public async Task<TodoAppAPIResponse<UserAuthenticated>> Authenticate(SignIn signInUser)
        {
            if (signInUser.Email == null && signInUser.Username == null)
                return new TodoAppAPIResponse<UserAuthenticated>("Invalid Account");

            var user = await _userRepository.GetByUsername(signInUser.Username);
            if (user == null)
                user = await _userRepository.GetByEmail(signInUser.Email);

            if (user == null)
                return new TodoAppAPIResponse<UserAuthenticated>("Invalid Account");

            if (user.Password == signInUser.Password)
            {
                var userToken = new UserAuthenticated
                {
                    Token = GenerateToken(user),
                    UserId = user.Id,
                    Username = user.Username,
                    Email = user.Email
                };
                return new TodoAppAPIResponse<UserAuthenticated>(userToken);
            }
            else
            {
                return new TodoAppAPIResponse<UserAuthenticated>("Invalid Account");
            }
        }

        public User GetById(int idUser)
        {
            return _userRepository.GetById(idUser);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.Secret);
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.GivenName, user.Name + user.Lastname),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var claim = new ClaimsIdentity();
            claim.AddClaims(claims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claim,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
