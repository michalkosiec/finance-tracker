using Api.Models;

namespace Api.Repositories.Interfaces
{
    public interface ITransactionRepo : IUserOwnedRepo<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllAsyncByUserId(TransactionParameters parameters, Guid userId);
        Task<decimal> GetTotalSpendingAsync(Guid userId, Guid categoryId, DateTime Month);
    }
}