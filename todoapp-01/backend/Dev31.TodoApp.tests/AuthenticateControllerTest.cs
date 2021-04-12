namespace Dev31.TodoApp.Tests
{
    using Dev31.TodoApp.API.Controllers;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Models;
    using Moq;
    using Dev31.TodoApp.Interfaces.Services;
    using Xunit;
    using Microsoft.AspNetCore.Mvc;
    using FluentAssertions;

    public class AuthenticateControllerTest
    {
        private Mock<IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>>> UserServiceMock;

        public AuthenticateControllerTest()
        {
            UserServiceMock = new Mock<IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>>>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_Ok_Response_When_Successful()
        {
            var signInResource = new SignIn()
            {
                Password = "asdQWE123",
                Email = "Carlos@gmail.com"
            };
            var user = new UserAuthenticated()
            {
                UserId = 15,
                Username = "Carl",
                Email = "Carlos@gmail.com",
                Token = "my token"
            };

            var userResponse = new TodoAppAPIResponse<UserAuthenticated>(user);

            UserServiceMock.Setup(UserService => UserService.Authenticate(It.IsAny<SignIn>())).ReturnsAsync(userResponse);

            var authenticateController = new AuthenticateController(UserServiceMock.Object);

            var result = await authenticateController.Authenticate(signInResource);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_Bad_Response_When_Password_Doesnt_Exist()
        {
            var signInResource = new SignIn()
            {
                Username = "Carlos"
            };

            var userResponse = new TodoAppAPIResponse<UserAuthenticated>("Invalid Account");

            UserServiceMock.Setup(UserService => UserService.Authenticate(It.IsAny<SignIn>())).ReturnsAsync(userResponse);

            var authenticateController = new AuthenticateController(UserServiceMock.Object);

            var result = await authenticateController.Authenticate(signInResource);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_Bad_Response_When_UserName_and_Email_Doesnt_Exist()
        {
            var signInResource = new SignIn()
            {
                Password = "asdQWE123"
            };

            var userResponse = new TodoAppAPIResponse<UserAuthenticated>("Invalid Account");

            UserServiceMock.Setup(UserService => UserService.Authenticate(It.IsAny<SignIn>())).ReturnsAsync(userResponse);

            var authenticateController = new AuthenticateController(UserServiceMock.Object);

            var result = await authenticateController.Authenticate(signInResource);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
