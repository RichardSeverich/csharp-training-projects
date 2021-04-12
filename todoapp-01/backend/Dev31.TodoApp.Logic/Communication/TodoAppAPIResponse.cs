// <copyright file="BaseResponse.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.Logic.Communication
{
    /// <summary>
    /// Class TagResponse extends BaseResponse
    /// </summary>
    public class TodoAppAPIResponse<T> where T : class
    {
        /// <summary>
        /// Constructor for the TodoAPIResponse
        /// </summary>
        /// <param name="success">Bool representing the success or not of the operation</param>
        /// <param name="message">String message explaining why the operation failed and showing the error message</param>
        /// <param name="entity">Instance of the class to be returned in the response</param>
        public TodoAppAPIResponse(bool success, string message, T entity) 
        {
            Entity = entity;
            Success = success;
            Message = message;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="entity">Instance of the class to be returned</param>
        /// <returns>Response.</returns>
        public TodoAppAPIResponse(T entity) : this(true, string.Empty, entity)
        {
        }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param message="message">Message given explaining why the operation failed</param>
        /// <returns>Response.</returns>
        public TodoAppAPIResponse(string message) : this(false, message, null)
        {
        }

        /// <summary>
        /// property Entity, Instance of the class to be returned in the response.
        /// </summary>
        public T Entity { get; private set; }

        /// <summary>
        /// property Message, message giving information of the failing in saving into the db if there is a fail.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// property Success, bool representing if the opertion was successful or not
        /// </summary>
        public bool Success { get; set; }
    }
}
