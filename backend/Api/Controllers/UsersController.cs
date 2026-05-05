using Api.Models;
using Api.Services;
using Api.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using AutoMapper;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(IUserRepo repo, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repo.GetAllAsync();
            var usersRead = mapper.Map<IEnumerable<UserReadDto>>(users);

            return Ok(usersRead);
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
                var userRead = mapper.Map<UserReadDto>(user);

                return Ok(userRead);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreate)
        {
            var user = mapper.Map<User>(userCreate);
            await repo.CreateAsync(user);
            var userRead = mapper.Map<UserReadDto>(user);

            return CreatedAtAction(nameof(GetUsers), new { id = user }, userRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDto userUpdate)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            } else
            {
                mapper.Map(userUpdate, user);
                user.UpdatedAt = DateTimeOffset.UtcNow;

                await repo.UpdateAsync(user);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null)
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