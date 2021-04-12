using Dev31.TodoApp.Interfaces.Repositories;
using Dev31.TodoApp.Logic.Communication;
using Dev31.TodoApp.Logic.Services;
using Dev31.TodoApp.Models;
using Moq;
using System;
using FluentAssertions;
using Xunit;

namespace Dev31.TodoApp.Tests
{
    public class TagServiceTest
    {
        [Fact]
        public async void DeleteAsync_Should_Return_Tag_Response_With_Message_Error_When_Input_Not_Exist()
        {
            var tagRepositoryMock = new Mock<IRepository<Tag, PostOptions, string>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            tagRepositoryMock.Setup(tagRepository => tagRepository.GetByPrimaryKeyAsync(It.IsAny<string>()));
            var projectService = new TagService(tagRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.DeleteAsync("tag unkown");

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<Tag>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Tag not found.");
        }

        [Fact]
        public async void DeleteAsync_Should_Return_Tag_Response_With_Tag_When_Input_Exist()
        {
            var name = "tag kown";
            var tag = new Tag() { Name = name };
            var tagRepositoryMock = new Mock<IRepository<Tag, PostOptions, string>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            tagRepositoryMock.Setup(tagRepository =>
                tagRepository.GetByPrimaryKeyAsync(It.Is<string>(t => t == name)))
                .ReturnsAsync(tag);
            tagRepositoryMock.Setup(tagRepository => tagRepository.Delete(It.IsAny<Tag>()));
            var projectService = new TagService(tagRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.DeleteAsync(name);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<Tag>>();
            result.Success.Should().BeTrue();
            result.Entity.Name.Should().Be(tag.Name);
        }

        [Fact]
        public async void SaveAsync_Should_Return_Tag_Response_With_New_Tag_When_Input_Is_Valid()
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
    }
}
