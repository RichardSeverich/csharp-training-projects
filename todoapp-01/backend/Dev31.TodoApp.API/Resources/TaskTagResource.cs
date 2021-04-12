// <copyright file="TaskTagResource.cs">
//    Copyright (c) jala.
// </copyright>
using Dev31.TodoApp.Models;

namespace Dev31.TodoApp.API.Resources
{
    /// <summary>
    /// Class TaskTagResource
    /// </summary>
    public class TaskTagResource
    {

        /// <summary>
        /// property Name_Tag, name of the related tag
        /// </summary>

        public int Id_Tag { get; set; }

        public TagResource Tag { get; set; }
    }
}
