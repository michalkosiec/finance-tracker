using Api.Models;
using Api.Repositories.Interfaces;

namespace Api.Repositories
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}