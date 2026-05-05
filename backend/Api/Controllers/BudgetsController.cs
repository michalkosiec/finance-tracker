using Api.Models;
using Api.Services;
using Api.Dtos.Budgets;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetsController(IBudgetRepo repo, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var budgets = await repo.GetAllAsync();
            var budgetsRead = mapper.Map<IEnumerable<BudgetReadDto>>(budgets);

            return Ok(budgetsRead);
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
                var budgetRead = mapper.Map<BudgetReadDto>(budget);

                return Ok(budgetRead);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] BudgetCreateDto budgetCreate)
        {
            var budget = mapper.Map<Budget>(budgetCreate);
            await repo.CreateAsync(budget);
            var budgetRead = mapper.Map<BudgetReadDto>(budget);

            return CreatedAtAction(nameof(GetBudgets), new { id = budget }, budgetRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] BudgetUpdateDto budgetUpdate)
        {
            var budget = await repo.GetByIdAsync(id);
            if (budget == null)
            {
                return NotFound();
            } else
            {
                mapper.Map(budgetUpdate, budget);
                budget.UpdatedAt = DateTimeOffset.UtcNow;
                
                await repo.UpdateAsync(budget);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            var budget = await repo.GetByIdAsync(id);
            if (budget == null)
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