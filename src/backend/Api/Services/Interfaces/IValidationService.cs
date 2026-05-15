using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IValidationService
    {
        Task<bool> AllowTransaction(Transaction transaction, Guid userId);
        Task<bool> AllowBudget(Budget budget, Guid userId);
    }
}