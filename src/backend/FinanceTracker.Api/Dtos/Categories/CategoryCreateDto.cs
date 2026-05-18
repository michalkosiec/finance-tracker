using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Dtos.Categories
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name {get; set;}

        [Required]
        public string Icon {get; set;}

        [Required]
        public string Color {get; set;}
    }
}