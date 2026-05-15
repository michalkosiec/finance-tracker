using Api.Models;
using Api.Dtos.Transactions;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Api.Services;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController(ITransactionRepo repo, IMapper mapper, IValidationService validationService) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] TransactionQueryDto query)
        {
            var parameters = mapper.Map<TransactionParameters>(query);
            var transactions = await repo.GetAllByUserIdAsync(UserId!.Value, parameters);
            var transactionsRead = mapper.Map<IEnumerable<TransactionReadDto>>(transactions);

            return Ok(transactionsRead);
        }

        [HttpGet("{id}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var transaction = await repo.GetByIdAsync(id, UserId!.Value);
            if (transaction == null)
            {
                return NotFound();
            } else
            {
                var transactionRead = mapper.Map<TransactionReadDto>(transaction);

                return Ok(transactionRead);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDto transactionCreate)
        {
            var transaction = mapper.Map<Transaction>(transactionCreate);

            transaction.UserId = UserId!.Value;

                if (!await validationService.AllowTransaction(transaction, UserId!.Value))
                    {
                        return BadRequest("Budget limit amount exceeded.");
                    }
            
            await repo.CreateAsync(transaction);
            var transactionRead = mapper.Map<TransactionReadDto>(transaction);

            return CreatedAtAction(nameof(GetTransactions), new { id = transaction.Id }, transactionRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] TransactionUpdateDto transactionUpdate)
        {
            var transaction = await repo.GetByIdAsync(id, UserId!.Value);
            if (transaction == null)
            {
                return NotFound();
            } else
            {
                mapper.Map(transactionUpdate, transaction);

                if (!await validationService.AllowTransaction(transaction, UserId!.Value))
                    {
                        return BadRequest("Budget limit amount exceeded.");
                    }

                await repo.UpdateAsync(transaction, UserId!.Value);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var transaction = await repo.GetByIdAsync(id, UserId!.Value);
            if (transaction == null)
            {
                return NotFound();
            } else
            {
                await repo.DeleteAsync(id, UserId!.Value);
                
                return NoContent();
            }
        }
    }
}