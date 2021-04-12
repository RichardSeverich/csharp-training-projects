namespace Dev31.TodoApp.Tests
{
    using AutoMapper;
    using Dev31.TodoApp.API.Resources;
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Models;
    using Moq;
    using FluentAssertions;
    using Xunit;
    using Dev31.TodoApp.API.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class SignupControllerTest
    {
        private Mock<IMapper> MapperMock;
        private Mock<IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>>> SignupServiceMock;

        public SignupControllerTest()
        {
            MapperMock = new Mock<IMapper>();
            SignupServiceMock = new Mock<IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>>>();
        }

        [Fact]
        public async void Get_Async_Should_Return_A_True_Response_When_Fields_Exits()
        {
            var field = "email";
            var value = "charly@gmail.com";

            SignupServiceMock.Setup(signupService => signupService.VerifyUserFields(It.Is<string>(f => f == field), It.Is<string>(n => n == value))).ReturnsAsync(true);

            var signupController = new SignupController(SignupServiceMock.Object, MapperMock.Object);
            // Act
            var result = await signupController.VerifyFields(value, null);
            var okResult = result as OkObjectResult;
            var expectedResult = okResult.Value as bool?;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedResult.Should().Be(true);
        }

        [Fact]
        public async void Get_Async_Should_Return_A_False_Response_When_Fields_Not_Exits()
        {
            var field = "username";
            var value = "charly";

            SignupServiceMock.Setup(signupService => signupService
                .VerifyUserFields(It.Is<string>(f => f == field), It.Is<string>(n => n == value)))
                .ReturnsAsync(false);

            var signupController = new SignupController(SignupServiceMock.Object, MapperMock.Object);
            // Act
            var result = await signupController.VerifyFields(null, value);
            var okResult = result as OkObjectResult;
            var expectedResult = okResult.Value as bool?;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedResult.Should().Be(false);
        }

        [Fact]
        public async void Post_Async_Should_Return_A_ObjectResult_400_Response_When_It_Does_Not_Match_The_Model()
        {
            var saveUserResource = new SaveUserResource
            {
                Username = "cha",
                Password = "asdQWE",
                Name = "charly",
                Lastname = "meneces",
                Email = "charly@gmail.com"
            };

            var signupController = new SignupController(SignupServiceMock.Object, MapperMock.Object);
            signupController.ModelState.AddModelError("Username", "Min length 4");
            // Act
            var result = await signupController.SaveAsync(saveUserResource);
            var objectResult = result as ObjectResult;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ObjectResult>();
            objectResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void Post_Async_Should_Return_A_ObjectResult_BadRequest_Response_When_Save_Fails()
        {
            var saveUserResource = new SaveUserResource
            {
                Username = "char",
                Password = "asdQWE",
                Name = "charly",
                Lastname = "meneces",
                Email = "charly@gmail.com"
            };
            var user = new User();
            var response = new TodoAppAPIResponse<User>("Cannot Add User");

            MapperMock.Setup(mapper => mapper
                            .Map<SaveUserResource, User>(It.Is<SaveUserResource>(s => s == saveUserResource)))
                            .Returns(user);

            SignupServiceMock.Setup(signupService => signupService.SaveAsync(It.Is<User>(n => n == user))).ReturnsAsync(response);

            var signupController = new SignupController(SignupServiceMock.Object, MapperMock.Object);
            // Act
            var result = await signupController.SaveAsync(saveUserResource);
            var objectResult = result as ObjectResult;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            objectResult.Value.Should().Be("Cannot Add User");
        }

        [Fact]
        public async void Post_Async_Should_Return_A_ObjectResult_Ok_Response_When_Save_Fails()
        {
            var saveUserResource = new SaveUserResource
            {
                Username = "cha",
                Password = "asdQWE123",
                Name = "charly",
                Lastname = "meneces",
                Email = "charly@gmail.com"
            };
            var user = new User();
            var response = new TodoAppAPIResponse<User>(user);

            MapperMock.Setup(mapper => mapper
                            .Map<SaveUserResource, User>(It.Is<SaveUserResource>(s => s == saveUserResource)))
                            .Returns(user);

            SignupServiceMock.Setup(signupService => signupService.SaveAsync(It.Is<User>(n => n == user))).ReturnsAsync(response);

            var signupController = new SignupController(SignupServiceMock.Object, MapperMock.Object);
            // Act
            var result = await signupController.SaveAsync(saveUserResource);
            var objectResult = result as ObjectResult;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            objectResult.Value.Should().Be(user);
        }
    }
}
