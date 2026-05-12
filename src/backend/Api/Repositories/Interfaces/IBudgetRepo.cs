using Api.Models;

namespace Api.Repositories.Interfaces
{
    public interface IBudgetRepo : IUserOwnedRepo<Budget>
    {
        Task<Budget?> GetByCategoryAsync(Guid id);
    }
}