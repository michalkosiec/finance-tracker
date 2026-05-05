using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController(ICategoryRepo repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await repo.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await repo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            } else
            {
                return Ok(category);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            await repo.CreateAsync(category);
            return CreatedAtAction(nameof(GetCategories), new { id = category }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] Category category)
        {
            var existingCategory = await repo.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            } else
            {
                await repo.UpdateAsync(category);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var existingCategory = await repo.GetByIdAsync(id);
            if (existingCategory == null)
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