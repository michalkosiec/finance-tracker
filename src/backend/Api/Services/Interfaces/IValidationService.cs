using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IValidationService
    {
        Task AllowTransaction(Transaction transaction, Guid userId);
        Task AllowBudget(Budget budget, Guid userId);
        Task AllowCategory(Category category, Guid userId);
        Task AllowCategoryDelete(Category category, Guid userId);
    }
}