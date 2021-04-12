namespace Dev31.TodoApp.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model SigIn .
    /// </summary>
    public class SignIn
    {
        /// <summary>
        /// property Username of the user
        /// </summary>
        [MinLength(4)]
        public string Username { get; set; }

        /// <summary>
        /// property Password of the user
        /// </summary>
        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// property Email of the user
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }
}
