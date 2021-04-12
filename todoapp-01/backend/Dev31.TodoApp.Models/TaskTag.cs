// <copyright file="TaskTag.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model TaskTag, Many to many relationship table
    /// </summary>
    public class TaskTag
    {
        /// <summary>
        /// property Id_task, Id of the related task
        /// </summary>
        [Required]
        public int Id_task { get; set; }

        /// <summary>
        /// property Task, instance of the related task
        /// </summary>
        public TodoTask Task { get; set; }

        /// <summary>
        /// property Name_Tag, name of the related tag
        /// </summary>
        [Required]
        public int Id_Tag { get; set; }

        /// <summary>
        /// property Tag, instance of the related Tag
        /// </summary>
        public Tag Tag { get; set; }
    }
}
