using Api.Models;

namespace Api.Repositories.Interfaces
{
    public interface IBudgetRepo : IUserOwnedRepo<Budget>
    {
        public Task<IEnumerable<Budget>> GetAllAsync(Guid id);
        Task<Budget?> GetByIdAsync(Guid id, Guid userId);
    }
}