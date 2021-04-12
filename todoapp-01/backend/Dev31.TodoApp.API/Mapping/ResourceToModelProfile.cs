// <copyright file="ResourceToModelProfile.cs">
//    Copyright (c) jala.
// </copyright> 
namespace Dev31.TodoApp.API.Mapping
{
    using AutoMapper;

    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.API.Resources;

    /// <summary>
    /// Class ResourceToModelProfile
    /// </summary>
    public class ResourceToModelProfile : Profile
    {
        /// <summary>
        /// Constructor for the ResourceToModelProfile
        /// </summary>
        /// <see cref="ProjectsController"/>
        public ResourceToModelProfile()
        {
            CreateMap<SaveProjectResource, Project>();
            CreateMap<SaveTagResource, Tag>();
            CreateMap<SaveTaskResource, TodoTask>();
            CreateMap<SaveUserResource, User>();
            CreateMap<ModifyTaskResource, TodoTask>();
            CreateMap<TaskTagResource, TaskTag>();
            CreateMap<SaveTaskTagResource, TaskTag>();
        }
    }
}
