// <copyright file="SaveTagResource.cs">
//    Copyright (c) jala.
// </copyright> 
namespace Dev31.TodoApp.API.Resources
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class SaveTagResource
    /// </summary>
    public class SaveTagResource
    {
        /// <summary>
        /// property Name, Name of the current tag
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        //public int Id { get; set; }
    }
}
