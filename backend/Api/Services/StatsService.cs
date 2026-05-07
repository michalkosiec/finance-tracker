using Api.Data;
using Api.Dtos.Stats;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class StatsService(AppDbContext context) : IStatsService
    {
         public async Task<StatsSummaryReadDto> GetSummaryAsync(DateTime date, Guid userId)
        {
            var monthlyQuery = context.Transactions.AsNoTracking().Where(t => t.UserId == userId && t.Date.Month == date.Month && t.Date.Year == date.Year);

            var totalIncome = await monthlyQuery.Where(t => t.Type == TransactionType.Income).SumAsync(t => t.Amount);
            var totalExpense = await monthlyQuery.Where(t => t.Type == TransactionType.Expense).SumAsync(t => t.Amount);

            return new StatsSummaryReadDto
            {
                Month = date.ToString("yyyy-MM"),
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = totalIncome - totalExpense
            };
        }
        public async Task<IEnumerable<CategoryStatReadDto>> GetExpensesByCategoryAsync(DateTime date, Guid userId)
        {
            var categoryStats = await context.Transactions.AsNoTracking().Where(t => t.UserId == userId &&
                t.Date.Month == date.Month &&
                t.Date.Year == date.Year &&
                t.Type == TransactionType.Expense)
                .GroupBy(t => t.Category!.Name)
                .Select(g => new CategoryStatReadDto { 
                    Category = g.Key, 
                    TotalExpense = g.Sum(t => t.Amount), 
                    NumberOfTransactions = g.Count()})
                .ToListAsync();
            
            return categoryStats;
        }

        public async Task<IEnumerable<MonthlyStatReadDto>> GetMonthlyStatsAsync(DateTime date, Guid userId)
        {
            var dbStats = await context.Transactions.AsNoTracking().Where(t => t.UserId == userId && t.Date.Year == date.Year)
                .GroupBy(t => t.Date.Month)
                .Select(g => new { 
                    Month = g.Key, 
                    TotalIncome = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
                    TotalExpense = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount),})
                .ToListAsync();

            var fullYearStats = new List<MonthlyStatReadDto>();

            for (int i = 1; i <= 12; i++)
            {
                var monthData = dbStats.FirstOrDefault(m => m.Month == i);

                fullYearStats.Add(new MonthlyStatReadDto {
                    Month = $"{date.Year}-{i:D2}",
                    TotalIncome = monthData?.TotalIncome ?? 0m,
                    TotalExpense = monthData?.TotalExpense ?? 0m,
                });
            }

            return fullYearStats;
        }
    }
}