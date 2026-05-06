using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Budgets
{
    public class BudgetUpdateDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "LimitAmount must be greater than 0.")]
        public decimal LimitAmount { get; set; }

        [Required]
        public string Month { get; set; }
    }
}