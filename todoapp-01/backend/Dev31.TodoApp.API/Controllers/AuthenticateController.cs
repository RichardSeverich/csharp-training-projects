namespace Dev31.TodoApp.API.Controllers
{
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.Utilities.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticateController: ControllerBase
    {
        public AuthenticateController(IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>> signInService)
        {
            _signInService = signInService;
        }

        [AllowAnonymous]
        [HttpPost]
        [EnableCors("TodoAppPolicy")]
        public async Task<IActionResult> Authenticate([FromBody] SignIn resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _signInService.Authenticate(resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Entity);
        }

        private IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>> _signInService;
    }
}
