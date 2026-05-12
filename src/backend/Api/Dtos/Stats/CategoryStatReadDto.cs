using System.ComponentModel.DataAnnotations;
using Api.Validations;

namespace Api.Dtos.Stats
{
    public class CategoryStatReadDto
    {
        [Required]
        [YearMonth]
        public string Month { get; set; } = string.Empty;
        
        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal TotalExpense { get; set; }

        [Required]
        public int NumberOfTransactions { get; set; }
    }
}