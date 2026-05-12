using Api.Models;

namespace Api.Services
{
    public interface IBudgetValidationService
    {
        Task<bool> AllowTransaction(Transaction transaction, Guid userId);
    }
}