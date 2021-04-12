using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Dev31.TodoApp.API.Resources;
using Dev31.TodoApp.Interfaces.Services;
using Dev31.TodoApp.Logic.Communication;
using Dev31.TodoApp.Models;
using Dev31.TodoApp.Utilities.Extensions;

namespace Dev31.TodoApp.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SignupController : ControllerBase
    {
        public SignupController(IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>> userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet()]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> VerifyFields([FromQuery( Name = "email" )] string email, [FromQuery(Name = "username")] string username)
        {
            bool result = true;

            if (email != null)
                result = await _userService.VerifyUserFields("email", email);

            if (username != null)
                result = await _userService.VerifyUserFields("username", username);

            return Ok(result);
        }

        [HttpPost]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> SaveAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Entity);
        }

        private IMapper _mapper;
        private IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>> _userService;
    }
}
