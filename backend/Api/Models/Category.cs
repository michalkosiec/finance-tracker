using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Category
    {
        [Key]
        [Required]
        public Guid Id {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public string Icon {get; set;}

        [Required]
        public string Color {get; set;}

        public bool IsActive {get; set;} = true;

        [Required]
        public DateTimeOffset CreatedAt {get; set;} = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset UpdatedAt {get; set;} = DateTimeOffset.UtcNow;
    }
}