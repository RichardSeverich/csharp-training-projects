namespace Dev31.TodoApp.Tests
{
    using Dev31.TodoApp.Interfaces.Repositories;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Logic.Services;
    using Dev31.TodoApp.Models;
    using Moq;
    using System;
    using FluentAssertions;
    using Xunit;

    public class UserServiceTest
    {
        [Fact]
        public async void SaveAsync_Should_Return_A_User_Ok_Response_When_Successful()
        {
            var user = new User()
            {
                Username = "char",
                Password = "secret123",
                Name = "charly",
                Lastname = "meneces",
                Email = "charly@gmail.com",
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.AddAsync(It.IsAny<User>()));
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            userRepositoryMock.Setup(userRepository => userRepository.GetByUsername(It.Is<string>(u => u == user.Username)))
                .ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.SaveAsync(user);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<User>>();
            result.Success.Should().BeTrue();
            result.Entity.Name.Should().Be(user.Name);
        }

        [Fact]
        public async void SaveAsync_Should_Return_A_User_Bad_Response_When_Occurred_Exception()
        {
            var user = new User()
            {
                Name = "charly",
                Lastname = "meneces",
                Email = "charly@gmail.com",
                CreatedAt = ""
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.AddAsync(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Cannot Add User"));

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.SaveAsync(user);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<User>>();
            result.Success.Should().BeFalse();
            result.Entity.Should().Be(null);
        }

        [Fact]
        public async void VerifyUserFields_Should_Return_A_Bool_True_When_It_Is_Sent_An_Email_That_Does_Not_Exist()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.GetByEmail(It.IsAny<string>()));

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.VerifyUserFields("email", "");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void VerifyUserFields_Should_Return_A_Bool_True_When_It_Is_Sent_An_Username_That_Does_Not_Exist()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.GetByUsername(It.IsAny<string>()));

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.VerifyUserFields("username", "");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void VerifyUserFields_Should_Return_A_Bool_False_When_It_Is_Sent_A_User_That_Exist()
        {
            var user = new User() { Username = "charly" };
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.GetByUsername(It.IsAny<string>())).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.VerifyUserFields("username", "charly");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void Authenticate_Should_Return_A_UserAuthenticated_Bad_Response_When_It_Doesnt_Send_Username_And_Email()
        {
            var signIn = new SignIn()
            {
                Password = "asdQWE123"
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.Authenticate(signIn);

            // Assert
            result.Message.Should().NotBeNull();
            result.Entity.Should().Be(null);
            result.Should().BeOfType<TodoAppAPIResponse<UserAuthenticated>>();
            result.Success.Should().BeFalse();
        }

            [Fact]
        public async void Authenticate_Should_Return_A_UserAuthenticated_Bad_Response_When_User_Exists_But_Password_Is_Not_Correct()
        {
            var user = new User()
            {
                Username = "char",
                Password = "secret123",
                Name = "charly",
                Lastname = "meneces",
                Email = "charly@gmail.com",
            };
            var signIn = new SignIn() 
            { 
                Username = "charly",
                Password = "asdQWE123"
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.GetByUsername(It.IsAny<string>())).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.Authenticate(signIn);

            // Assert
            result.Message.Should().NotBeNull();
            result.Entity.Should().Be(null);
            result.Should().BeOfType<TodoAppAPIResponse<UserAuthenticated>>();
            result.Success.Should().BeFalse();
        }

        [Fact]
        public async void Authenticate_Should_Return_A_UserAuthenticated_Ok_Response_When_User_Exists_And_Password_Is_Correct()
        {
            var user = new User()
            {
                Username = "char",
                Password = "secret123",
                Name = "charly",
                Lastname = "meneces",
                Email = "charly@gmail.com",
            };
            var signIn = new SignIn()
            {
                Password = "secret123",
                Email = "charly@gmail.com"
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.GetByEmail(It.IsAny<string>())).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.Authenticate(signIn);

            // Assert
            result.Message.Should().BeEmpty();
            result.Entity.Should().NotBeNull();
            result.Should().BeOfType<TodoAppAPIResponse<UserAuthenticated>>();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async void Authenticate_Should_Return_A_UserAuthenticated_Ok_Response_When_A_User_Is_Submitted_And_Cannot_Find_It()
        {
            var signIn = new SignIn()
            {
                Password = "secret123",
                Email = "charly@gmail.com"
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.GetByEmail(It.IsAny<string>()));
            userRepositoryMock.Setup(userRepository => userRepository.GetByUsername(It.IsAny<string>()));

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await userService.Authenticate(signIn);

            // Assert
            result.Message.Should().Be("Invalid Account");
            result.Entity.Should().BeNull();
            result.Should().BeOfType<TodoAppAPIResponse<UserAuthenticated>>();
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void GetById_Should_Return_A_User_When_User_Exists()
        {
            var user = new User()
            {
                Id = 2,
                Username = "char",
                Password = "secret123",
                Name = "charly",
                Lastname = "meneces",
                Email = "charly@gmail.com",
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(userRepository => userRepository.GetById(It.IsAny<int>())).Returns(user);

            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = userService.GetById(2);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
        }
    }
}
