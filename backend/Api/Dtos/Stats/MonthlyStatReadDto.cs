using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Stats
{
    public class MonthlyStatReadDto
    {
        [Required]
        public string Month { get; set; } = string.Empty;

        [Required]
        public decimal TotalIncome { get; set; }

        [Required]
        public decimal TotalExpense { get; set; }
    }
}