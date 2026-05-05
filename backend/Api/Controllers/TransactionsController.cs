using Api.Models;
using Api.Services;
using Api.Dtos.Transactions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController(ITransactionRepo repo, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await repo.GetAllAsync();
            var transactionsRead = mapper.Map<IEnumerable<TransactionReadDto>>(transactions);

            return Ok(transactionsRead);
        }

        [HttpGet("{id}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var transaction = await repo.GetByIdAsync(id);
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
            await repo.CreateAsync(transaction);
            var transactionRead = mapper.Map<TransactionReadDto>(transaction);

            return CreatedAtAction(nameof(GetTransactions), new { id = transaction }, transactionRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] TransactionUpdateDto transactionUpdate)
        {
            var transaction = await repo.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            } else
            {
                mapper.Map(transactionUpdate, transaction);
                await repo.UpdateAsync(transaction);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var transaction = await repo.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            } else
            {
                await repo.DeleteAsync(id);
                return NoContent();
            }
        }
    }
}