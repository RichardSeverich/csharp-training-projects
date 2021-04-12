// <copyright file="SaveTaskResource.cs">
//    Copyright (c) jala.
// </copyright> 
namespace Dev31.TodoApp.API.Resources
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class SaveTaskResource
    /// </summary>
    public class SaveTaskResource
    {
        /// <summary>
        /// property Description, description of the task
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// property Priority, String demoting the priority of the task
        /// </summary>
        [Required]
        public string Priority { get; set; }

        /// <summary>
        /// property Status, String, tatus of the current task
        /// </summary>
        [Required]
        [MaxLength(11)]
        public string Status { get; set; }

        /// <summary>
        /// property Due, String, Start date of the task
        /// </summary>
        [MaxLength(27)]
        public string Start { get; set; }

        /// <summary>
        /// property Due, String, Due date of the task
        /// </summary>
        [MaxLength(27)]
        public string Due { get; set; }

        /// <summary>
        /// property Due, String, End date of the task
        /// </summary>
        [MaxLength(27)]
        public string End { get; set; }

        /// <summary>
        /// property Due, String, Entry date of the task
        /// </summary>
        [MaxLength(27)]
        public string Entry { get; set; }

        /// <summary>
        /// property Depends, Identifier of a task that the current task depends of
        /// </summary>
        public int? Depends { get; set; }

        /// <summary>
        /// property ProjectUuid, Guid uuid of the project the task belongs to
        /// </summary>
        public Guid ProjectUuid { get; set; }

        /// <summary>
        /// property Tags, List with instances of the relation between tasks and tags
        /// </summary>
        public ICollection<SaveTaskTagResource> Tags { get; set; }
    }
}
