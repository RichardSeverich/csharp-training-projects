using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FluentAssertions;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Dev31.TodoApp.Logic.Communication;
using Dev31.TodoApp.Interfaces.Services;
using System.Threading.Tasks;
using Dev31.TodoApp.Models;
using Dev31.TodoApp.API.Resources;
using Dev31.TodoApp.API.Controllers;


namespace Dev31.TodoApp.Tests
{
    public class TaskControllerTest
    {
        private Mock<IMapper> MapperMock;
        private Mock<ITaskService<TodoAppAPIResponse<TodoTask>>> TaskServiceMock;

        public TaskControllerTest()
        {
            MapperMock = new Mock<IMapper>();
            TaskServiceMock = new Mock<ITaskService<TodoAppAPIResponse<TodoTask>>>();
        }

        [Fact]
        public async void Get_All_Async_Should_Return_A_List_Of_Task_Resources()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };
            var taskResource = new TaskResource()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                Tags = new List<TaskTagResource>
                {
                    new TaskTagResource
                    {
                        Id_Tag = 2,
                        Tag = new TagResource()
                    }
                }
            };

            var tasks = new List<TodoTask>() { task };
            var taskResources = new List<TaskResource>() { taskResource };
            var pagedList = PagedList<TodoTask>.Create(tasks, 1, 1);
            TaskServiceMock.Setup(TaskService => TaskService.ListAsync(It.IsAny<PostOptions>())).ReturnsAsync(pagedList);
            MapperMock.Setup(mapper => mapper
                      .Map<IEnumerable<TodoTask>, IEnumerable<TaskResource>>(It.Is<IEnumerable<TodoTask>>(t => t == pagedList)))
                      .Returns(taskResources);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);

            //Act
            var result = await taskController.GetAllAsync(new PostOptions());
            var okResult = result as OkObjectResult;


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            okResult.Value.Should().BeOfType<TasksResource>();
        }

        [Fact]
        public async void Get_By_Uuid_Async_Should_Return_A_Ok_Response_When_Task_Exists()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };
            var taskResource = new TaskResource()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                Tags = new List<TaskTagResource>
                {
                    new TaskTagResource
                    {
                        Id_Tag = 2,
                        Tag = new TagResource()
                    }
                }
            };
            TaskServiceMock.Setup(taskService => taskService.FindAsync(It.Is<Guid>(t => t == uuid))).ReturnsAsync(task);
            MapperMock.Setup(mapper => mapper.Map<TodoTask, TaskResource>(It.Is<TodoTask>(t => t == task))).Returns(taskResource);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.GetAsync(uuid);
            var okResult = result as OkObjectResult;
            var expectedTask = okResult.Value as TaskResource;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedTask.Should().Be(taskResource);
            expectedTask.Description.Should().Be(taskResource.Description);
        }

        [Fact]
        public async void Get_By_Uuid_Async_Should_Return_A_BadResponse_When_Task_Doesnt_Exist()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            TaskServiceMock.Setup(taskService => taskService.FindAsync(It.Is<Guid>(t => t == uuid)));
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.GetAsync(uuid);
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Update_Status_Async_Should_Return_A_Ok_Response_When_Exist_Case_In_Progress()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var taskStatus = new TodoTask()
            {
                Status = "In Progress",
            };

            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "In Progress",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };
            var taskResource = new TaskResource()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                Tags = new List<TaskTagResource>
                {
                    new TaskTagResource
                    {
                        Id_Tag = 2,
                        Tag = new TagResource()
                    }
                }
            };

            var response = new TodoAppAPIResponse<TodoTask>(task);

            TaskServiceMock.Setup(taskService => taskService
                           .UpdateStatusAsync(It.Is<Guid>(t => t == uuid), It.Is<TodoTask>(t => t == taskStatus)))
                           .ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper.Map<TodoTask, TaskResource>(It.Is<TodoTask>(t => t == response.Entity))).Returns(taskResource);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.UpdateStatusAsync(uuid, taskStatus);
            var okResult = result as OkObjectResult;
            var expectedTask = okResult.Value as TaskResource;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedTask.Should().Be(taskResource);
            expectedTask.Description.Should().Be(taskResource.Description);
        }

        [Fact]
        public async void Update_Status_Async_Should_Return_A_Ok_Response_When_Exist_Case_Completed()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var taskStatus = new TodoTask()
            {
                Status = "Completed",
            };

            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Completed",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = "2020-12-12T00:00:00Z",
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };
            var taskResource = new TaskResource()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "In Progress",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                Tags = new List<TaskTagResource>
                {
                    new TaskTagResource
                    {
                        Id_Tag = 2,
                        Tag = new TagResource()
                    }
                }
            };

            var response = new TodoAppAPIResponse<TodoTask>(task);

            TaskServiceMock.Setup(taskService => taskService
                           .UpdateStatusAsync(It.Is<Guid>(t => t == uuid), It.Is<TodoTask>(t => t == taskStatus)))
                           .ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper.Map<TodoTask, TaskResource>(It.Is<TodoTask>(t => t == response.Entity))).Returns(taskResource);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.UpdateStatusAsync(uuid, taskStatus);
            var okResult = result as OkObjectResult;
            var expectedTask = okResult.Value as TaskResource;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedTask.Should().Be(taskResource);
            expectedTask.Description.Should().Be(taskResource.Description);
        }

        [Fact]
        public async void Update_Status_Async_Should_Return_A_Ok_Response_When_Exist_Case_Deleted()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var taskStatus = new TodoTask()
            {
                Status = "Deleted",
            };

            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Deleted",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = "2020-12-12T00:00:00Z",
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };
            var taskResource = new TaskResource()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Completed",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = "2020-12-12T00:00:00Z",
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                Tags = new List<TaskTagResource>
                {
                    new TaskTagResource
                    {
                        Id_Tag = 2,
                        Tag = new TagResource()
                    }
                }
            };

            var response = new TodoAppAPIResponse<TodoTask>(task);

            TaskServiceMock.Setup(taskService => taskService
                           .UpdateStatusAsync(It.Is<Guid>(t => t == uuid), It.Is<TodoTask>(t => t == taskStatus)))
                           .ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper.Map<TodoTask, TaskResource>(It.Is<TodoTask>(t => t == response.Entity))).Returns(taskResource);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.UpdateStatusAsync(uuid, taskStatus);
            var okResult = result as OkObjectResult;
            var expectedTask = okResult.Value as TaskResource;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedTask.Should().Be(taskResource);
            expectedTask.Description.Should().Be(taskResource.Description);
        }

        [Fact]
        public async void Update_Status_Async_Should_Return_A_BadResponse_When_Task_Doesnt_Exist()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var taskStatus = new TodoTask()
            {
                Status = "In Progress",
            };

            var response = new TodoAppAPIResponse<TodoTask>("Error Message");

            TaskServiceMock.Setup(taskService => taskService
                           .UpdateStatusAsync(It.Is<Guid>(t => t == uuid), It.Is<TodoTask>(t => t == taskStatus)))
                           .ReturnsAsync(response);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.UpdateStatusAsync(uuid, taskStatus);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Delete_Async_Should_Return_A_Ok_Response_When_Task_Exists()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");

            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Deleted",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = "2020-12-12T00:00:00Z",
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };
            var taskResource = new TaskResource()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Deleted",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = "2020-12-12T00:00:00Z",
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                Tags = new List<TaskTagResource>
                {
                    new TaskTagResource
                    {
                        Id_Tag = 2,
                        Tag = new TagResource()
                    }
                }
            };

            var response = new TodoAppAPIResponse<TodoTask>(task);

            TaskServiceMock.Setup(taskService => taskService
                           .DeleteAsync(It.Is<Guid>(t => t == uuid)))
                           .ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper.Map<TodoTask, TaskResource>(It.Is<TodoTask>(t => t == response.Entity))).Returns(taskResource);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.DeleteAsync(uuid);
            var okResult = result as OkObjectResult;
            var expectedTask = okResult.Value as TaskResource;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedTask.Should().Be(taskResource);
            expectedTask.Description.Should().Be(taskResource.Description);
        }

        [Fact]
        public async void Delete_Async_Should_Return_A_BadResponse_When_Task_Doesnt_Exist()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");

            var response = new TodoAppAPIResponse<TodoTask>("Error Message");

            TaskServiceMock.Setup(taskService => taskService
                           .DeleteAsync(It.Is<Guid>(t => t == uuid)))
                           .ReturnsAsync(response);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.DeleteAsync(uuid);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_Ok_Response_When_Model_State_Is_Valid()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
            };
            var taskResource = new SaveTaskResource()
            {
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = null,
                ProjectUuid = new Guid("F7B80BE9-455A-4A90-0BE9-3CF044F713F1"),
                Tags = null

            };
            var response = new TodoAppAPIResponse<TodoTask>(task);
            TaskServiceMock.Setup(taskService => taskService
                           .SaveAsync(It.Is<TodoTask>(t => t == task)))
                           .ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper.Map<SaveTaskResource, TodoTask>(It.Is<SaveTaskResource>(t => t == taskResource))).Returns(task);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.PostAsync(taskResource);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_BadResponse_When_Saving_Fails()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
            };
            var taskResource = new SaveTaskResource()
            {
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = null,
                ProjectUuid = new Guid("F7B80BE9-455A-4A90-0BE9-3CF044F713F1"),
                Tags = null
            };
            var response = new TodoAppAPIResponse<TodoTask>("Error Message");
            TaskServiceMock.Setup(taskService => taskService
                           .SaveAsync(It.Is<TodoTask>(t => t == task)))
                           .ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper.Map<SaveTaskResource, TodoTask>(It.Is<SaveTaskResource>(t => t == taskResource))).Returns(task);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.PostAsync(taskResource);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Put_Async_Should_Return_A_Ok_Response_When_Model_State_Is_Valid()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var modifyTaskResource = new ModifyTaskResource()
            {
                Description = "My todo task",
                Priority = "Medium",
                Due = "2020-12-13T00:00:00Z",
                Tags = null,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294")
            };

            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Deleted",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };
            var taskResource = new TaskResource()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Deleted",
                Start = "2020-12-12T00:00:00Z",
                Due = null,
                End = "2020-12-12T00:00:00Z",
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                Tags = new List<TaskTagResource>()
            };
            var response = new TodoAppAPIResponse<TodoTask>(task);
            MapperMock.Setup(mapper => mapper
                            .Map<ModifyTaskResource, TodoTask>(It.Is<ModifyTaskResource>(t => t == modifyTaskResource)))
                            .Returns(task);
            TaskServiceMock.Setup(taskService => taskService
                           .UpdateAsync(It.Is<Guid>(t => t == uuid), It.Is<TodoTask>(t => t == task)))
                           .ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper
                            .Map<TodoTask, TaskResource>(It.Is<TodoTask>(t => t == response.Entity)))
                            .Returns(taskResource);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.PutAsync(uuid, modifyTaskResource);
            var okResult = result as OkObjectResult;
            var expectedTask = okResult.Value as TaskResource;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            expectedTask.Should().Be(taskResource);
            expectedTask.Description.Should().Be(taskResource.Description);
        }

        [Fact]
        public async void Put_Async_Should_Return_A_BadResponse_When_Failing_To_Save()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var modifyTaskResource = new ModifyTaskResource()
            {
                Description = "My todo task",
                Priority = "Medium",
                Due = "2020-12-13T00:00:00Z",
                Tags = null,
                ProjectUuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294")
            };

            var task = new TodoTask()
            {
                Id = 1,
                Uuid = uuid,
                Description = "My todo task",
                Priority = "Medium",
                Status = "Deleted",
                Start = null,
                Due = null,
                End = null,
                Entry = "2020-12-12T00:00:00Z",
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };
            var response = new TodoAppAPIResponse<TodoTask>("Error Message");
            MapperMock.Setup(mapper => mapper
                            .Map<ModifyTaskResource, TodoTask>(It.Is<ModifyTaskResource>(t => t == modifyTaskResource)))
                            .Returns(task);
            TaskServiceMock.Setup(taskService => taskService
                           .UpdateAsync(It.Is<Guid>(t => t == uuid), It.Is<TodoTask>(t => t == task)))
                           .ReturnsAsync(response);
            var taskController = new TasksController(TaskServiceMock.Object, MapperMock.Object);
            // Act
            var result = await taskController.PutAsync(uuid, modifyTaskResource);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
