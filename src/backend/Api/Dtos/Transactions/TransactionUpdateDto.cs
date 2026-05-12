using System.ComponentModel.DataAnnotations;
using Api.Models;
using Api.Validations;

namespace Api.Dtos.Transactions
{
    public class TransactionUpdateDto
    {
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
        [YearMonthDay]
        public string Date {get; set;}

        [Required]
        public TransactionType Type {get; set;}
    }
}