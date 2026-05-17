using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Category : IUserOwned
    {
        [Key]
        [Required]
        public Guid Id {get; set;}

        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category name must be between {2} and {1} characters.")]
        public string Name {get; set;}

        [Required]
        public string Icon {get; set;}

        [Required]
        [StringLength(20, ErrorMessage = "Color value cannot exceed {1} characters.")]
        public string Color {get; set;}

        [Required]
        public DateTimeOffset CreatedAt {get; set;} = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset UpdatedAt {get; set;} = DateTimeOffset.UtcNow;
    }
}