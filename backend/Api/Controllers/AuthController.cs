using Api.Dtos.Users;
using Api.Services;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using Api.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService, IUserRepo repo, IMapper mapper) : AppControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            var token = await authService.LoginAsync(userLogin);

            if (token is null)
            {
                return Unauthorized(new { message = "Wrong email or password." });
            }

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreate)
        {
            if (!await authService.RegisterAsync(userCreate))
            {
                return BadRequest(new {message = "User is already registered."});
            }
            
            return Ok(new {message = "User was successfully registered."});
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = UserId;
            if (userId is null) return Unauthorized();

            var user = await repo.GetByIdAsync(userId.Value);
            var userRead = mapper.Map<UserReadDto>(user);

            return Ok(userRead);
        }
    }
}