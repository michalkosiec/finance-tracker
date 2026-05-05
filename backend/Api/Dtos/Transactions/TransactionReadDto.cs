using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Dtos.Transactions
{
    public class TransactionReadDto
    {
         [Required]
        public Guid Id {get; set;}

        [Required]
        public Guid UserId {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public decimal Amount {get; set;}

        [Required]
        public string Currency {get; set;}

        [Required]
        public Guid CategoryId {get; set;}

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