using System.Globalization;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class TransactionRepo(AppDbContext context) : GenericRepo<Transaction>(context), ITransactionRepo
    {
        public async Task<IEnumerable<Transaction>> GetAllAsync(TransactionParameters parameters)
        {
            var query = context.Transactions.Include(t => t.Category).AsNoTracking();

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