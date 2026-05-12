using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class BudgetRepo(AppDbContext context) : UserOwnedRepo<Budget>(context), IBudgetRepo
    {
        public async Task<Budget?> GetByCategoryAsync(Guid id)
        {
            return await context.Budgets.FirstOrDefaultAsync(b => b.CategoryId == id);
        }
    }
}