using AutoMapper;
using Dev31.TodoApp.API.Resources;
using Dev31.TodoApp.Interfaces.Services;
using Dev31.TodoApp.Logic.Communication;
using Dev31.TodoApp.Models;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using Dev31.TodoApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Dev31.TodoApp.Tests
{
    public class TagControllerTest
    {
        private Mock<IMapper> MapperMock;
        private Mock<ITagService<TodoAppAPIResponse<Tag>>> TagServiceMock;

        public TagControllerTest()
        {
            MapperMock = new Mock<IMapper>();
            TagServiceMock = new Mock<ITagService<TodoAppAPIResponse<Tag>>>();
        }

        [Fact]
        public async void Get_All_Should_Return_A_List_Of_Tag_Resources()
        {
            var taskTags = new List<TaskTag>();
            var taskTagsResource = new List<TaskTagResource>();
            var tag = new Tag()
            {
                Name = "Tag1",
                Tasks = taskTags
            };
            var tagResource = new TagResource()
            {
                Id = 1,
                Name = "Tag1",
                Tasks = taskTagsResource
            };
            var tags = new List<Tag>() { tag };
            var tagResources = new List<TagResource>() { tagResource };
            TagServiceMock.Setup(TagService => TagService.ListAsync()).ReturnsAsync(tags);
            MapperMock.Setup(mapper => mapper
                      .Map<IEnumerable<Tag>, IEnumerable<TagResource>>(It.Is<IEnumerable<Tag>>(t => t == tags)))
                      .Returns(tagResources);
            MapperMock.Setup(mapper => mapper
                      .Map<ICollection<TaskTag>, ICollection<TaskTagResource>>(It.Is<ICollection<TaskTag>>(t => t == taskTags)))
                      .Returns(taskTagsResource);
            var tagController = new TagsController(TagServiceMock.Object, MapperMock.Object);
            // Act
            var result = await tagController.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeSameAs(tagResources);
            result.Should().BeOfType<List<TagResource>>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_Ok_Response_When_Save_Is_Successful()
        {
            var saveTagResource = new SaveTagResource()
            {
                Name = "Tag1"
            };
            var tag = new Tag()
            {
                Name = "Tag",
                Tasks = new List<TaskTag>()
            };

            var response = new TodoAppAPIResponse<Tag>(tag);

            TagServiceMock.Setup(tagService => tagService.SaveAsync(It.IsAny<Tag>())).ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper
                      .Map<SaveTagResource, Tag>(It.IsAny<SaveTagResource>()))
                      .Returns(tag);
            var tagController = new TagsController(TagServiceMock.Object, MapperMock.Object);
            // Act
            var result = await tagController.PostAsync(saveTagResource);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Post_Async_Should_Return_A_Bad_Response_When_Save_Is_Unsuccessful()
        {
            var saveTagResource = new SaveTagResource()
            {
                Name = "Tag1"
            };
            var tag = new Tag()
            {
                Name = "Tag",
                Tasks = new List<TaskTag>()
            };

            var response = new TodoAppAPIResponse<Tag>("Error");

            TagServiceMock.Setup(tagService => tagService.SaveAsync(It.IsAny<Tag>())).ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper
                      .Map<SaveTagResource, Tag>(It.IsAny<SaveTagResource>()))
                      .Returns(tag);
            var tagController = new TagsController(TagServiceMock.Object, MapperMock.Object);
            // Act
            var result = await tagController.PostAsync(saveTagResource);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void Delete_Async_Should_Return_A_Ok_Response_When_Delete_Is_Successful()
        {
            var tagResource = new TagResource()
            {
                Id = 2,
                Name = "Tag1",
                Tasks = new List<TaskTagResource>()
            };
            var tag = new Tag()
            {
                Name = "Tag",
                Tasks = new List<TaskTag>()
            };

            var response = new TodoAppAPIResponse<Tag>(tag);

            TagServiceMock.Setup(tagService => tagService.DeleteAsync(It.Is<String>(n => n == tag.Name))).ReturnsAsync(response);
            MapperMock.Setup(mapper => mapper
                      .Map<Tag, TagResource>(It.IsAny<Tag>()))
                      .Returns(tagResource);
            var tagController = new TagsController(TagServiceMock.Object, MapperMock.Object);
            // Act
            var result = await tagController.DeleteAsync(tag.Name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void Delete_Async_Should_Return_A_Bad_Response_When_Delete_Is_Unsuccessful()
        {
            var tag = new Tag()
            {
                Name = "Tag",
                Tasks = new List<TaskTag>()
            };

            var response = new TodoAppAPIResponse<Tag>("Error");

            TagServiceMock.Setup(tagService => tagService.DeleteAsync(It.Is<String>(n => n == tag.Name))).ReturnsAsync(response);
 
            var tagController = new TagsController(TagServiceMock.Object, MapperMock.Object);
            // Act
            var result = await tagController.DeleteAsync(tag.Name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
