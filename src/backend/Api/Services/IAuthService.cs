using Api.Dtos.Users;

namespace Api.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(UserLoginDto userLogin);
        Task<bool> RegisterAsync(UserCreateDto userCreate);
    }
}