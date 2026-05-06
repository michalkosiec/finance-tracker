using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.Dtos.Transactions
{
    public class TransactionCreateDto
    {
        [Required]
        public Guid UserId {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public decimal Amount {get; set;}

        [Required]
        [StringLength(3)]
        public string Currency {get; set;}

        [Required]
        public Guid CategoryId {get; set;}

        [Required]
        public string Date {get; set;}

        [Required]
        public TransactionType Type {get; set;}
    }
}