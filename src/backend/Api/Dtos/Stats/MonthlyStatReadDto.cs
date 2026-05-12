using System.ComponentModel.DataAnnotations;
using Api.Validations;

namespace Api.Dtos.Stats
{
    public class MonthlyStatReadDto
    {
        [Required]
        [YearMonth]
        public string Month { get; set; } = string.Empty;

        [Required]
        public decimal TotalIncome { get; set; }

        [Required]
        public decimal TotalExpense { get; set; }
    }
}