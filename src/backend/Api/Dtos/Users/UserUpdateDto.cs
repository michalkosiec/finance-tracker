using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Users
{
    public class UserUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}