using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Budget
    {
        [Key]
        [Required]
        public Guid Id {get; set;}

        [Required]
        public Guid UserId {get; set;}
        public User? User {get; set;}

        [Required]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal LimitAmount { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        
        [Required]
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}