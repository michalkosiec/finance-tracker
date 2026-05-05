using Api.Models;

namespace Api.Services
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}