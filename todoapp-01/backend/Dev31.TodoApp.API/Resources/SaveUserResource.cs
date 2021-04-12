namespace Dev31.TodoApp.API.Resources
{
    using System.ComponentModel.DataAnnotations;
    public class SaveUserResource
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
