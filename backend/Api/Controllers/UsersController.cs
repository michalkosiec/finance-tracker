using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserRepo repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repo.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            } else
            {
                return Ok(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await repo.CreateAsync(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {
            var existingUser = await repo.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            } else
            {
                await repo.UpdateAsync(user);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var existingUser = await repo.GetByIdAsync(id);
            if (existingUser == null)
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