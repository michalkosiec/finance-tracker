using Api.Models;
using Api.Services;
using Api.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController(ICategoryRepo repo, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await repo.GetAllAsync();
            var categoriesRead = mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return Ok(categoriesRead);
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
                var categoryRead = mapper.Map<CategoryReadDto>(category);

                return Ok(categoryRead);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreate)
        {
            var category = mapper.Map<Category>(categoryCreate);
            await repo.CreateAsync(category);
            var categoryRead = mapper.Map<CategoryReadDto>(category);

            return CreatedAtAction(nameof(GetCategories), new { id = category }, categoryRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryUpdate)
        {
            var category = await repo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                mapper.Map(categoryUpdate, category);
                category.UpdatedAt = DateTimeOffset.UtcNow;
                
                await repo.UpdateAsync(category);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await repo.GetByIdAsync(id);
            if (category == null)
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