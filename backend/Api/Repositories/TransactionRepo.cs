using System.Globalization;
using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class TransactionRepo(AppDbContext context) : UserOwnedRepo<Transaction>(context), ITransactionRepo
    {
        public async Task<IEnumerable<Transaction>> GetAllAsyncByUserId(TransactionParameters parameters, Guid userId)
        {
            var query = context.Transactions.Include(t => t.Category).AsNoTracking().Where(c => c.UserId == userId);

            if (parameters.Month is not null)
            {
                var date = DateTime.SpecifyKind(DateTime.ParseExact(parameters.Month, "yyyy-MM", CultureInfo.InvariantCulture), DateTimeKind.Utc);
                query = query.Where(t => t.Date.Month == date.Month && t.Date.Year == date.Year);
            }

            if (parameters.CategoryId is not null)
            {
                query = query.Where(t => t.CategoryId == parameters.CategoryId);
            }

            if (parameters.Type is not null)
            {
                query = query.Where(t => t.Type == parameters.Type);
            }

            return await query.ToListAsync();
        }
    }

}