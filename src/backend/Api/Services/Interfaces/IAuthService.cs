using Api.Dtos.Users;

namespace Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(UserLoginDto userLogin);
        Task<bool> RegisterAsync(UserCreateDto userCreate);
    }
}