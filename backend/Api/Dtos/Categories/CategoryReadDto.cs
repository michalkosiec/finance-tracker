using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Categories
{
    public class CategoryReadDto
    {
       [Required]
        public Guid Id {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public string Icon {get; set;}

        [Required]
        public string Color {get; set;}

        public bool IsActive {get; set;}

        [Required]
        public DateTimeOffset CreatedAt {get; set;}

        [Required]
        public DateTimeOffset UpdatedAt {get; set;}
    }
}