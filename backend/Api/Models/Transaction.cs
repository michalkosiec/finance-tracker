using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Transaction
    {
        [Key]
        [Required]
        public Guid Id {get; set;}

        [Required]
        public Guid UserId {get; set;}
        public User? User {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public decimal Amount {get; set;}

        [Required]
        [StringLength(3)]
        public string Currency {get; set;}

        [Required]
        public Guid CategoryId {get; set;}
        public Category? Category {get; set;}

        [Required]
        public DateTime Date {get; set;}

        [Required]
        public TransactionType Type {get; set;}

        [Required]
        public string Title {get; set;}

        [Required]
        public DateTimeOffset CreatedAt {get; set;}

        [Required]
        public DateTimeOffset UpdatedAt {get; set;}
    }
}