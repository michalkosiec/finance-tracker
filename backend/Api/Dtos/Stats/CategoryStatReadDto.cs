using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Stats
{
    public class CategoryStatReadDto
    {
        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal TotalExpense { get; set; }

        [Required]
        public int NumberOfTransactions { get; set; }
    }
}