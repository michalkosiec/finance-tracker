using System.ComponentModel.DataAnnotations;
using Api.Validations;

namespace Api.Dtos.Budgets
{
    public class BudgetCreateDto
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "LimitAmount must be greater than 0.")]
        public decimal LimitAmount { get; set; }

        [Required]
        [YearMonth]
        public string Month { get; set; }
    }
}