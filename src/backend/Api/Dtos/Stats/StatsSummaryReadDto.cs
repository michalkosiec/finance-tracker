using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Stats
{
    public class StatsSummaryReadDto
    {
        [Required]
        public string Month { get; set; } = string.Empty;

        [Required]
        public decimal TotalIncome { get; set; }

        [Required]
        public decimal TotalExpense { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}