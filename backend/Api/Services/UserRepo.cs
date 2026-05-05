using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class UserRepo(AppDbContext context) : GenericRepo<User>(context), IUserRepo
    {
        public async Task<User?> GetUserByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}