// <copyright file="TagResource.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.API.Resources
{
    using System.Collections.Generic;

    /// <summary>
    /// Class TagResource
    /// </summary>
    public class TagResource
    {
        /// <summary>
        /// property Id, Id of the task, given by the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// property Name of the tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// property Tasks, List of instances of The resource of the relation between tasks and tags
        /// </summary>
        public ICollection<TaskTagResource> Tasks {get; set;}
    }
}
