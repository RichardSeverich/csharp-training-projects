using Dev31.TodoApp.Interfaces.Repositories;
using Dev31.TodoApp.Logic.Communication;
using Dev31.TodoApp.Logic.Services;
using Dev31.TodoApp.Models;
using Moq;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Dev31.TodoApp.Tests
{
    public class ProjectServiceTest
    {
        [Fact]
        public async void DeleteAsync_Should_Return_Project_Response_With_Message_Error_When_Input_Not_Exist()
        {
            var project = Guid.NewGuid();
            var projectRepositoryMock = new Mock<IRepository<Project, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            projectRepositoryMock.Setup(projectRepository => projectRepository.GetByPrimaryKeyAsync(It.IsAny<Guid>()));
            var projectService = new ProjectService(projectRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.DeleteAsync(project);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<Project>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Project not found.");
        }

        [Fact]
        public async void DeleteAsync_Should_Return_Project_Response_With_Project_When_Input_Exist()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var project = new Project { Uuid = uuid, Name = "My deleted project" };
            var projectRepositoryMock = new Mock<IRepository<Project, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            projectRepositoryMock.Setup(projectRepository =>
                projectRepository.GetByPrimaryKeyAsync(It.Is<Guid>(p => p == uuid))
                ).ReturnsAsync(project);
            projectRepositoryMock.Setup(projectRepository =>
                projectRepository.GetChildren(It.Is<Guid>(p => p == uuid))
                ).ReturnsAsync(new List<Project>());
            projectRepositoryMock.Setup(projectRepository => projectRepository.Delete(It.IsAny<Project>()));
            var projectService = new ProjectService(projectRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.DeleteAsync(uuid);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<Project>>();
            result.Success.Should().BeTrue();
            result.Entity.Uuid.Should().Be(uuid);
            result.Entity.Name.Should().Be(project.Name);
        }

        [Fact]
        public async void FindAsync_Should_Return_Project_When_Input_Exist()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var project = new Project { Uuid = uuid, Name = "My project" };
            var projectRepositoryMock = new Mock<IRepository<Project, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            projectRepositoryMock.Setup(projectRepository =>
                projectRepository.GetByPrimaryKeyAsync(It.Is<Guid>(p => p == uuid))
                ).ReturnsAsync(project);
            var projectService = new ProjectService(projectRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.FindAsync(uuid);

            // Assert
            result.Should().BeOfType<Project>();
            result.Uuid.Should().Be(uuid);
            result.Name.Should().Be(project.Name);
        }

        [Fact]
        public async void FindAsync_Should_Return_Null_When_Input_Not_Exist()
        {
            Project project = null;
            var uuid = Guid.NewGuid();
            var projectRepositoryMock = new Mock<IRepository<Project, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            projectRepositoryMock.Setup(projectRepository =>
                projectRepository.GetByPrimaryKeyAsync(It.IsAny<Guid>())
                ).ReturnsAsync(project);
            var projectService = new ProjectService(projectRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.FindAsync(uuid);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void ListAsync_Should_Return_A_List_Of_Projects()
        {
            var projectRepositoryMock = new Mock<IRepository<Project, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            projectRepositoryMock.Setup(projectRepository =>
                projectRepository.GetAllAsync()
                ).ReturnsAsync(new List<Project>());
            var projectService = new ProjectService(projectRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.ListAsync();
            
            // Assert
            result.Should().BeOfType<List<Project>>();
        }

        [Fact]
        public async void SaveAsync_Should_Return_Project_Response_With_New_Project_When_Input_Is_Valid()
        {
            var project = new Project { Name = "My new project" };
            var projectRepositoryMock = new Mock<IRepository<Project, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            projectRepositoryMock.Setup(projectRepository => projectRepository.AddAsync(It.IsAny<Project>()));
            var projectService = new ProjectService(projectRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.SaveAsync(project);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<Project>>();
            result.Success.Should().BeTrue();
            result.Entity.Name.Should().Be(project.Name);
        }

        [Fact]
        public async void UpdateAsync_Should_Return_Project_Response_With_Message_Error_When_Input_Not_Exist()
        {
            var uuid = Guid.NewGuid();
            var newProject = new Project { Name = "My updated project" };
            var projectRepositoryMock = new Mock<IRepository<Project, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            projectRepositoryMock.Setup(projectRepository => projectRepository.GetByPrimaryKeyAsync(It.IsAny<Guid>()));
            var projectService = new ProjectService(projectRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.UpdateAsync(uuid, newProject);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<Project>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Project not found.");
        }

        [Fact]
        public async void UpdateAsync_Should_Return_Project_Response_With_Project_Updated_When_Input_Not_Exist()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var project = new Project { Uuid = uuid, Name = "My project" };
            var newProject = new Project { Name = "My updated project" };
            var projectRepositoryMock = new Mock<IRepository<Project, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            projectRepositoryMock.Setup(projectRepository =>
                projectRepository.GetByPrimaryKeyAsync(It.Is<Guid>(p => p == uuid))
                ).ReturnsAsync(project);
            projectRepositoryMock.Setup(projectRepository => projectRepository.Update(It.IsAny<Project>()));
            var projectService = new ProjectService(projectRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.UpdateAsync(uuid, newProject);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<Project>>();
            result.Success.Should().BeTrue();
            result.Entity.Uuid.Should().Be(uuid);
            result.Entity.Name.Should().Be(newProject.Name);
        }
    }
}
