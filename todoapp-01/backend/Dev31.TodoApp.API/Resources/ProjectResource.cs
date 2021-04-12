// <copyright file="ProjectResource.cs">
//    Copyright (c) jala.
// </copyright> 
namespace Dev31.TodoApp.API.Resources
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class ProjectResource
    /// </summary>
    public class ProjectResource
    {
        /// <summary>
        /// property Uuid Guid, identifier of the project
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// property Name, Projects's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// property Parent, Uuid of the parent project the current project belongs to.
        /// </summary>
        public Guid Parent { get; set; }

        /// <summary>
        /// property Tasks, List of the tasks that belong to the current project
        /// </summary>
        public ICollection<TaskResource> Tasks { get; set; }
    }
}
