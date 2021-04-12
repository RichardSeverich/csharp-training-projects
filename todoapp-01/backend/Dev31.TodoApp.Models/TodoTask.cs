// <copyright file="TodoTask.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model TodoTask
    /// </summary>
    public class TodoTask
    {
        /// <summary>
        /// property Id, Id of the task, given by the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// property Uuid, Guid unique identifier of the task
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// property Description, description of the task
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// property Priority, String demoting the priority of the task
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// property Status, String, tatus of the current task
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// property Start, String, Start date of the task
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// property Due, String, Due date of the task
        /// </summary>
        public string Due { get; set; }

        /// <summary>
        /// property End, String, End date of the task
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// property Entry, String, Entry date of the task
        /// </summary>
        public string Entry { get; set; }

        /// <summary>
        /// property Tags, List with instances of the relation between tasks and tags
        /// </summary>
        public ICollection<TaskTag> Tags { get; set; }

        /// <summary>
        /// property Depends, Identifier of a task that the current task depends of
        /// </summary>
        public int? Depends { get; set; }

        /// <summary>
        /// property Project, instance of the Project the task belongs to
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// property ProjectUuid, Guid uuid of the project the task belongs to
        /// </summary>
        public Guid ProjectUuid { get; set; }
    }
}
