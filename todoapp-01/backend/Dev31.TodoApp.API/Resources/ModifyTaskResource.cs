// <copyright file="ModifyTaskResource.cs">
//    Copyright (c) jala.
// </copyright> 
namespace Dev31.TodoApp.API.Resources
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class ModifyTaskResource
    /// </summary>
    public class ModifyTaskResource
    {
        /// <summary>
        /// property Description, description of the task
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// property Priority, String demoting the priority of the task
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// property Due, String, Due date of the task
        /// </summary>
        [MaxLength(27)]
        public string Due { get; set; }

        /// <summary>
        /// property Tags, List with instances of the relation between tasks and tags
        /// </summary>
        public ICollection<SaveTaskTagResource> Tags { get; set; }

        /// <summary>
        /// property ProjectUuid, Guid uuid of the project the task belongs to
        /// </summary>
        public Guid ProjectUuid { get; set; }
    }
}
