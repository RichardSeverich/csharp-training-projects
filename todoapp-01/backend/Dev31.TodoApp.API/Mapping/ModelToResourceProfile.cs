// <copyright file="ModelToResourceProfile.cs">
//    Copyright (c) jala.
// </copyright> 
namespace Dev31.TodoApp.API.Mapping
{
    using AutoMapper;

    using Dev31.TodoApp.API.Resources;
    using Dev31.TodoApp.Models;

    /// <summary>
    /// Class ModelToResourceProfile
    /// </summary>
    public class ModelToResourceProfile : Profile
    {
        /// <summary>
        /// Constructor for the ModelToResourceProfile
        /// </summary>
        /// <see cref="ProjectsController"/>
        public ModelToResourceProfile()
        {
            CreateMap<Project, ProjectResource>();
            CreateMap<Tag, TagResource>();
            CreateMap<TodoTask, TaskResource>();
            CreateMap<TaskTag, TaskTagResource>();
        }
    }
}
