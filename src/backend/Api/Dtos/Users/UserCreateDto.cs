using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Users
{
    public class UserCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}