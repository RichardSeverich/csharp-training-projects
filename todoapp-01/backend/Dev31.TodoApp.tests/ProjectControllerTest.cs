using System;
using System.Collections.Generic;
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

namespace Dev31.TodoApp.Tests
{
    public class ProjectControllerTest
    {
        private Mock<IMapper> MapperMock;
        private Mock<IProjectService<TodoAppAPIResponse<Project>>> ProjectServiceMock;
        public ProjectControllerTest()
        {
            MapperMock = new Mock<IMapper>();
            ProjectServiceMock = new Mock<IProjectService<TodoAppAPIResponse<Project>>>();
        }

        [Fact]
        public async void Get_All_Should_Return_A_List_Of_Projet_Resources()
        {
            var todoTasks = new List<TodoTask>();
            var taskResource = new List<TaskResource>();
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var uuidPatern = new Guid("00000000-0000-0000-0000-000000000000");
            var project = new Project()
            {
                Uuid = uuid,
                Name = "project1",
                Parent = null,
                Tasks = todoTasks
            };
            var projectResource = new ProjectResource()
            {
                Uuid = uuid,
                Name = "project1",
                Parent = uuidPatern,
                Tasks = taskResource
            };
            var projects = new List<Project>() { project };
            var projectResources = new List<ProjectResource>() { projectResource };
            ProjectServiceMock.Setup(ProjectService => ProjectService.ListAsync()).ReturnsAsync(projects);
            MapperMock.Setup(mapper => mapper
                      .Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(It.Is<IEnumerable<Project>>(t => t == projects)))
                      .Returns(projectResources);
            MapperMock.Setup(mapper => mapper
                      .Map<ICollection<TodoTask>, ICollection<TaskResource>>(It.Is<ICollection<TodoTask>>(t => t == todoTasks)))
                      .Returns(taskResource);
            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeSameAs(projectResources);
            result.Should().BeOfType<List<ProjectResource>>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_Ok_Response_When_Save_Is_Successful()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var uuidPatern = new Guid("00000000-0000-0000-0000-000000000000");
            var saveProjectResource = new SaveProjectResource()
            {
                Name = "project1",
                Parent = uuid
            };
            var project = new Project()
            {
                Uuid = uuid,
                Name = "project1",
                Parent = uuidPatern,
                Tasks = new List<TodoTask>()
            };

            var response = new TodoAppAPIResponse<Project>(project);

            ProjectServiceMock.Setup(projectService => projectService.SaveAsync(It.IsAny<Project>())).ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper
                      .Map<SaveProjectResource, Project>(It.IsAny<SaveProjectResource>()))
                      .Returns(project);
            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.PostAsync(saveProjectResource);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_Bad_Response_When_Save_Is_Unsuccessful()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var uuidPatern = new Guid("00000000-0000-0000-0000-000000000000");
            var saveProjectResource = new SaveProjectResource()
            {
                Name = "project1",
                Parent = uuid
            };
            var project = new Project()
            {
                Uuid = uuid,
                Name = "project1",
                Parent = uuidPatern,
                Tasks = new List<TodoTask>()
            };

            var response = new TodoAppAPIResponse<Project>("Error");

            ProjectServiceMock.Setup(projectService => projectService.SaveAsync(It.IsAny<Project>())).ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper
                      .Map<SaveProjectResource, Project>(It.IsAny<SaveProjectResource>()))
                      .Returns(project);
            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.PostAsync(saveProjectResource);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Delete_Async_Should_Return_A_Ok_Response_When_Delete_Is_Successful()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var uuidPatern = new Guid("00000000-0000-0000-0000-000000000000");
            var projectResource = new ProjectResource()
            {
                Name = "project1",
                Parent = uuid
            };
            var project = new Project()
            {
                Uuid = uuid,
                Name = "project1",
                Parent = uuidPatern,
                Tasks = new List<TodoTask>()
            };

            var response = new TodoAppAPIResponse<Project>(project);

            ProjectServiceMock.Setup(projectService => projectService.DeleteAsync(It.Is<Guid>(n => n == project.Uuid))).ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper
                      .Map<Project, ProjectResource>(It.IsAny<Project>()))
                      .Returns(projectResource);
            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.DeleteAsync(project.Uuid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Delete_Async_Should_Return_A_Bad_Response_When_Delete_Is_Unsuccessful()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var uuidPatern = new Guid("00000000-0000-0000-0000-000000000000");
            var project = new Project()
            {
                Uuid = uuid,
                Name = "project1",
                Parent = uuidPatern,
                Tasks = new List<TodoTask>()
            };

            var response = new TodoAppAPIResponse<Project>("Error");

            ProjectServiceMock.Setup(projectService => projectService.DeleteAsync(It.Is<Guid>(n => n == project.Uuid))).ReturnsAsync(response);

            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.DeleteAsync(project.Uuid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Get_By_Uuid_Async_Should_Return_A_Ok_Response_When_Project_Exists()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var uuidParent = new Guid("00000000-0000-0000-0000-000000000000");
            var project = new Project()
            {
                Uuid = uuid,
                Name = "project1",
                Parent = null
            };
            var projectResource = new ProjectResource()
            {
                Name = "project1",
                Parent = uuidParent,
                Uuid = uuid,
                Tasks = null
            } ;
            ProjectServiceMock.Setup(projectService => projectService
                .FindAsync(It.Is<Guid>(p => p == uuid)))
                .ReturnsAsync(project);
            MapperMock.Setup(mapper => mapper
                .Map<Project, ProjectResource>(It.Is<Project>(t => t == project)))
                .Returns(projectResource);
            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.GetAsync(uuid);
            var okResult = result as OkObjectResult;
            var expectedProject = okResult.Value as ProjectResource;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedProject.Should().Be(projectResource);
            expectedProject.Name.Should().Be(projectResource.Name);
        }

        [Fact]
        public async void Get_By_Uuid_Async_Should_Return_A_BadResponse_When_Project_Doesnt_Exist()
        {
            var uuid = new Guid("00000000-0000-0000-0000-000000000000");
            ProjectServiceMock.Setup(projectService => projectService.FindAsync(uuid));
            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.GetAsync(uuid);
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void Put_Async_Should_Return_A_Ok_Response_When_Model_State_Is_Valid()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var uuidParent = new Guid("00000000-0000-0000-0000-000000000000");
            var saveProjectResource = new SaveProjectResource()
            {
                Name = "project1",
                Parent = uuidParent
            };

            var project = new Project()
            {
                Name = "project1",
                Uuid = uuid,
                Parent = uuidParent
            };

            var projectResource = new ProjectResource()
            {
                Name = "project1",
                Uuid = uuid,
                Parent = uuidParent
            };
            var response = new TodoAppAPIResponse<Project>(project);
            MapperMock.Setup(mapper => mapper
                            .Map<SaveProjectResource, Project>(It.Is<SaveProjectResource>(t => t == saveProjectResource)))
                            .Returns(project);
            ProjectServiceMock.Setup(projectService => projectService
                           .UpdateAsync(It.Is<Guid>(t => t == uuid), It.Is<Project>(p => p == project)))
                           .ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper
                            .Map<Project, ProjectResource>(It.Is<Project>(t => t == response.Entity)))
                            .Returns(projectResource);
            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.PutAsync(uuid, saveProjectResource);
            var okResult = result as OkObjectResult;
            var expectedProject = okResult.Value as Project;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedProject.Uuid.Should().Be(projectResource.Uuid);
            expectedProject.Name.Should().Be(projectResource.Name);
        }

        [Fact]
        public async void Put_Async_Should_Return_A_BadResponse_When_Failing_To_Save()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var uuidParent = new Guid("00000000-0000-0000-0000-000000000000");
            var saveProjectResource = new SaveProjectResource()
            {
                Parent = uuid,
                Name = "project1",
            };

            var project = new Project()
            {
                Uuid = uuid,
                Name = "project1",
                Tasks = null,
                Parent = uuidParent
            };
            var response = new TodoAppAPIResponse<Project>("Error Message");
            MapperMock.Setup(mapper => mapper
                            .Map<SaveProjectResource, Project>(It.Is<SaveProjectResource>(t => t == saveProjectResource)))
                            .Returns(project);
            ProjectServiceMock.Setup(projectService => projectService
                           .UpdateAsync(It.Is<Guid>(t => t == uuid), It.Is<Project>(t => t == project)))
                           .ReturnsAsync(response);
            var projectController = new ProjectsController(ProjectServiceMock.Object, MapperMock.Object);
            // Act
            var result = await projectController.PutAsync(uuid, saveProjectResource);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
