using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Categories
{
    public class CategoryUpdateDto
    {
        [Required]
        public string Name {get; set;}

        [Required]
        public string Icon {get; set;}

        [Required]
        public string Color {get; set;}
    }
}