namespace Dev31.TodoApp.Models
{
    using System.Collections.Generic;

    public class PostOptions
    {
        /// <summary>
        /// property PageSize, Int, Size of page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// property PageNumber, Int, Number of page
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// property Description, String, Description of the task
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// property Priority, String demoting the priority of the task
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// property Status, String, status of the current task
        /// </summary>
        public ICollection<string> Status { get; set; }

        /// <summary>
        /// property Tags, List with instances of the relation between tasks and tags
        /// </summary>
        public ICollection<string> Tags { get; set; }

        /// <summary>
        /// property ProjectUuid, Guid uuid of the project the task belongs to
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// property Entry, String, Entry date of the task
        /// </summary>
        public string Entry { get; set; }
    }
}
