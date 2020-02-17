using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts
{
    public class CreateUser
    {
        [Required]
        public string Name { get; set; }

        [Required, StringLength(3)]
        public string Username { get; set; }

        [Required, StringLength(3)]
        public string Password { get; set; }

        [Required, StringLength(3)]
        public string ConfirmPassword { get; set; }
    }
}