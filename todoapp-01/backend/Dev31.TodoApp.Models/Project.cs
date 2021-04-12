// <copyright file="Project.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model Project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// property Uuid, Guid unique identifier of the project
        /// </summary>
        [Required]
        public Guid Uuid { get; set; }

        /// <summary>
        /// property Name of the project
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// property Parent Uuid of the parent project the current project belongs to.
        /// </summary>
        public Guid? Parent { get; set; }

        /// <summary>
        /// property Tasks belonging to the current project
        /// </summary>
        public ICollection<TodoTask> Tasks { get; set; }
    }
}
