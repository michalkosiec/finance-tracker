using Api.Dtos.Stats;

namespace Api.Services
{
    public interface IStatsService
    {
        Task<StatsSummaryReadDto> GetSummaryAsync(DateTime date, Guid userId);
        Task<IEnumerable<CategoryStatReadDto>> GetExpensesByCategoryAsync(DateTime date, Guid userId);
        Task<MonthlyStatsReadDto> GetMonthlyStatsAsync(DateTime date, Guid userId);
    }
}