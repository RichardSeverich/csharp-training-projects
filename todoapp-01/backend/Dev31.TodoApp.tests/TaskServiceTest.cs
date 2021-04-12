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
    public class TaskServiceTest
    {
        [Fact]
        public async void DeleteAsync_Should_Return_Task_Response_With_Message_Error_When_Input_Not_Exist()
        {
            var task = Guid.NewGuid();
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.GetByPrimaryKeyAsync(It.IsAny<Guid>()));
            var taskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await taskService.DeleteAsync(task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Task not found.");
        }

        [Fact]
        public async void DeleteAsync_Should_Return_Task_Response_With_Task_When_Input_Exist()
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

            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup( taskRepository => 
                taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid))
            ).ReturnsAsync(task);
            taskRepositoryMock.Setup(taskRepository => taskRepository.Delete(It.IsAny<TodoTask>()));
            var projectService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await projectService.DeleteAsync(uuid);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeTrue();
            result.Entity.Uuid.Should().Be(uuid);
            result.Entity.Id.Should().Be(task.Id);
            result.Entity.Description.Should().Be(task.Description);
            result.Entity.Status.Should().Be("Deleted");
            result.Entity.Priority.Should().Be(task.Priority);
            result.Entity.End.Should().NotBeNull();
        }

        [Fact]
        public async void FindAsync_Should_Return_Task_When_Input_Exist()
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
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository =>
                taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid))
                ).ReturnsAsync(task);
            var taskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await taskService.FindAsync(uuid);

            // Assert
            result.Should().BeOfType<TodoTask>();
            result.Uuid.Should().Be(uuid);
            result.Id.Should().Be(task.Id);
            result.Description.Should().Be(task.Description);
            result.Status.Should().Be(task.Status);
            result.Priority.Should().Be(task.Priority);
            result.End.Should().BeNull();
            result.Start.Should().BeNull();
            result.Due.Should().BeNull();
        }

        [Fact]
        public async void FindAsync_Should_Return_Null_When_Input_Not_Exist()
        {
            TodoTask task = null;
            var uuid = Guid.NewGuid();
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository =>
                taskRepository.GetByPrimaryKeyAsync(It.IsAny<Guid>())
                ).ReturnsAsync(task);
            var taskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await taskService.FindAsync(uuid);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void ListAsync_Should_Return_A_List_Of_Tasks()
        {
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository =>
                taskRepository.GetAllAsync(It.IsAny<PostOptions>())
                ).ReturnsAsync(new List<TodoTask>());
            var taskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);
            var options = new PostOptions
            {
                PageNumber = 1,
                PageSize = 5,
                Description = "See",
                Entry = "day",
                Priority = "Low",
                Project = null,
                Status = new List<string> { "N!Pending" },
                Tags = new List<string> { "N!1" }
            };

            // Act
            var result = await taskService.ListAsync(options);

            // Assert
            result.Should().BeOfType<PagedList<TodoTask>>();
        }

        [Fact]
        public async void SaveAsync_Should_Return_Task_Response_With_New_Task_When_Input_Is_Valid()
        {
            var taskToSave = new TodoTask()
            {
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Due = null,
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };

            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.AddAsync(It.IsAny<TodoTask>()));
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.SaveAsync(taskToSave);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeTrue();
            result.Entity.Should().NotBeNull();
            result.Entity.Description.Should().Be(taskToSave.Description);
            result.Entity.Status.Should().Be(taskToSave.Status);
            result.Entity.Uuid.Should().NotBeEmpty();
            result.Entity.Entry.Should().NotBeEmpty();
        }

        [Fact]
        public async void SaveAsync_Should_Return_A_Error_Message_When_Save_On_UnitOf_Work_Fails()
        {
            var taskToSave = new TodoTask()
            {
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Due = null,
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };

            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync()).ThrowsAsync(new Exception("Task Wasn't Able to save"));
            taskRepositoryMock.Setup(taskRepository => taskRepository.AddAsync(It.IsAny<TodoTask>()));
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.SaveAsync(taskToSave);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeFalse();
            result.Entity.Should().BeNull();
        }

        [Fact]
        public async void SaveAsync_Should_Return_A_Error_Message_When_Repository_Fails()
        {
            var taskToSave = new TodoTask()
            {
                Description = "My todo task",
                Priority = "Medium",
                Status = "Pending",
                Due = null,
                Depends = 0,
                Project = new Project()
                {
                    Uuid = new Guid("494c7fbc-0fde-4230-a15c-d5bb903f8294"),
                    Name = "My project"
                }
            };

            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.AddAsync(It.IsAny<TodoTask>())).ThrowsAsync(new Exception("Cannot Add Task"));
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.SaveAsync(taskToSave);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeFalse();
            result.Entity.Should().BeNull();
        }

        [Fact]
        public async void UpdateAsync_Should_Return_Task_Response_With_Message_Error_When_Input_Not_Exist()
        {
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F9");
            var task = new TodoTask()
            {
                Priority = "Medium",
                Status = "Pending",
                Due = null,
                Depends = 0,
            };
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>()));
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);
            // Act
            var result = await TaskService.UpdateAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Task not found.");
        }

        [Fact]
        public async void UpdateAsync_Should_Return_Task_Response_With_Task_Updated_When_Input_Exist()
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
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>()));
            taskRepositoryMock.Setup(taskRepository =>
                taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid))
                ).ReturnsAsync(task);
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);
            
            // Act
            var result = await TaskService.UpdateAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeTrue();
            result.Entity.Should().BeOfType<TodoTask>();
            result.Entity.Description.Should().Be(task.Description);
            result.Entity.Uuid.Should().Be(task.Uuid);
            result.Entity.Priority.Should().Be(task.Priority);
            result.Entity.Status.Should().Be(task.Status);
            result.Entity.Start.Should().Be(task.Start);
            result.Entity.End.Should().Be(task.End);
            result.Entity.Entry.Should().Be(task.Entry);
            result.Entity.Depends.Should().Be(task.Depends);

        }

        [Fact]
        public async void UpdateAsync_Should_Return_Task_Response_With_Error_Message_When_UnitOfWork_Fails()
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
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync()).ThrowsAsync(new Exception("Error"));
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>()));
            taskRepositoryMock.Setup(taskRepository =>
                taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid))
                ).ReturnsAsync(task);
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.UpdateAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("An error occurred when updating the task: Error");
        }

        [Fact]
        public async void UpdateAsync_Should_Return_Task_Response_With_Error_Message_When_Repository_Fails()
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
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>())).Throws(new Exception("Error"));
            taskRepositoryMock.Setup(taskRepository =>
                taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid))
                ).ReturnsAsync(task);
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.UpdateAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("An error occurred when updating the task: Error");
        }

        [Fact]
        public async void UpdateStatusAsync_Should_Return_Task_Response_With_Message_Error_When_Input_Not_Exist()
        {
            // Arrange
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Status = "Completed",
            };
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>()));
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            //Act
            var result = await TaskService.UpdateStatusAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Task not found.");
        }
        [Fact]
        public async void UpdateStatusAsync_Should_Return_Task_Response_With_Task_Updated_When_Input_Exist_Status_Completed()
        {
            // Arrange
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Status = "Completed",
            };
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>()));
            taskRepositoryMock.Setup(taskRepository =>
                                     taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid)))
                                    .ReturnsAsync(task);
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.UpdateStatusAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeTrue();
            result.Entity.Status.Should().Be(task.Status);
            result.Entity.End.Should().BeOfType<String>();
        }
        [Fact]
        public async void UpdateStatusAsync_Should_Return_Task_Response_With_Task_Updated_When_Input_Exist_Status_In_Progress()
        {
            // Arrange
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Status = "In Progress",
            };
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>()));
            taskRepositoryMock.Setup(taskRepository =>
                                     taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid)))
                                    .ReturnsAsync(task);
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.UpdateStatusAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeTrue();
            result.Entity.Status.Should().Be(task.Status);
            result.Entity.Start.Should().BeOfType<String>();
        }

        [Fact]
        public async void UpdateStatusAsync_Should_Return_Task_Response_With_Task_Updated_When_Input_Exist_Status_Deleted()
        {
            // Arrange
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Status = "Deleted",
            };
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>()));
            taskRepositoryMock.Setup(taskRepository =>
                                     taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid)))
                                    .ReturnsAsync(task);
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.UpdateStatusAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeTrue();
            result.Entity.Status.Should().Be(task.Status);
            result.Entity.End.Should().BeOfType<String>();
        }

        [Fact]
        public async void UpdateStatusAsync_Should_Return_Task_Response_With_Task_Updated_When_Input_Exist_Status_Unknown()
        {
            // Arrange
            var uuid = new Guid("F7B80BE9-455A-4A90-A0ED-3CF044F713F1");
            var task = new TodoTask()
            {
                Status = "Unknown",
            };
            var taskRepositoryMock = new Mock<IRepository<TodoTask, PostOptions, Guid>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.CompleteAsync());
            taskRepositoryMock.Setup(taskRepository => taskRepository.Update(It.IsAny<TodoTask>()));
            taskRepositoryMock.Setup(taskRepository =>
                                     taskRepository.GetByPrimaryKeyAsync(It.Is<Guid>(t => t == uuid)))
                                    .ReturnsAsync(task);
            var TaskService = new TaskService(taskRepositoryMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await TaskService.UpdateStatusAsync(uuid, task);

            // Assert
            result.Should().BeOfType<TodoAppAPIResponse<TodoTask>>();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Status unknown");
        }
    }
}
