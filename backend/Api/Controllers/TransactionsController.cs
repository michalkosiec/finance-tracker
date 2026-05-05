using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController(ITransactionRepo repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await repo.GetAllAsync();
            return Ok(transactions);
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
                return Ok(transaction);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            await repo.CreateAsync(transaction);
            return CreatedAtAction(nameof(GetTransactions), new { id = transaction }, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] Transaction transaction)
        {
            var existingTransaction = await repo.GetByIdAsync(id);
            if (existingTransaction == null)
            {
                return NotFound();
            } else
            {
                await repo.UpdateAsync(transaction);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var existingTransaction = await repo.GetByIdAsync(id);
            if (existingTransaction == null)
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