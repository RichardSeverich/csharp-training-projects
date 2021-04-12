// <copyright file="TasksResource.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.API.Resources
{
    using System.Collections.Generic;

    /// <summary>
    /// Class TasksResource
    /// </summary>
    public class TasksResource
    {
        /// <summary>
        /// property Id, Id of the task, given by the database
        /// </summary>
        public IEnumerable<TaskResource> Data { get; set; }
        
        /// <summary>
        /// property CurrentPage, Number of current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// property PageSize, Size of the pages
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// property NextPageNumber, Next page of the current page
        /// </summary>
        public int? NextPageNumber { get; set; }

        /// <summary>
        /// property TotalPages, Total pages, result of total count divided by page size
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// property TotalCount, Total count of results
        /// </summary>public int PageSize { get; set; }
        public int TotalCount { get; set; }

        /// <summary>
        /// property PreviousPageNumber, Previous page of the current page
        /// </summary>public int? NextPageNumber { get; set; }
        public int? PreviousPageNumber { get; set; }
    }
}
