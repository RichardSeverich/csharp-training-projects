// <copyright file="Tag.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model Tag
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// property Id, Id of the task, given by the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// property Name of the tag
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// property Tasks List with instances of the relation between tasks and tags
        /// </summary>
        public ICollection<TaskTag> Tasks { get; set; }
    }
}
