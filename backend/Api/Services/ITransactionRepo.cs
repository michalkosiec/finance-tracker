using Api.Models;

namespace Api.Services
{
    public interface ITransactionRepo : IGenericRepo<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllAsync(TransactionParameters parameters);
    }
}