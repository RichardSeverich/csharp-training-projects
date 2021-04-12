// <copyright file="SaveProjectResource.cs">
//    Copyright (c) jala.
// </copyright> 
namespace Dev31.TodoApp.API.Resources
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class SaveProjectResource
    /// </summary>
    public class SaveProjectResource
    {
        /// <summary>
        /// property Name, Projects's name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// property Parent, Uuid of the parent project the current project belongs to.
        /// </summary>
        public Guid Parent { get; set; }
    }
}
