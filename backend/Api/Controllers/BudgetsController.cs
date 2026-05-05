using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetsController(IBudgetRepo repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var budgets = await repo.GetAllAsync();
            return Ok(budgets);
        }

        [HttpGet("{id}", Name = "GetBudgetById")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            var budget = await repo.GetByIdAsync(id);
            if (budget == null)
            {
                return NotFound();
            } else
            {
                return Ok(budget);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] Budget budget)
        {
            await repo.CreateAsync(budget);
            return CreatedAtAction(nameof(GetBudgets), new { id = budget }, budget);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] Budget budget)
        {
            var existingBudget = await repo.GetByIdAsync(id);
            if (existingBudget == null)
            {
                return NotFound();
            } else
            {
                await repo.UpdateAsync(budget);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            var existingBudget = await repo.GetByIdAsync(id);
            if (existingBudget == null)
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