using Api.Models;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;

namespace Api.Services
{
    public class ValidationService(IBudgetRepo budgetRepo, ITransactionRepo transactionRepo, ICategoryRepo categoryRepo) : IValidationService
    {
        public async Task<bool> AllowBudget(Budget budget, Guid userId)
        {
            var budgetId = budget.Id;
            var categoryId = budget.CategoryId;
            var limitAmount = budget.LimitAmount;
            var month = budget.Month.Month;
            var year = budget.Month.Year;

            if(await budgetRepo.AnyAsync(b => 
                b.Id != budgetId &&
                b.CategoryId == categoryId &&
                b.Month.Month == month &&
                b.Month.Year == year &&
                b.UserId == userId))
            {
                throw new BadHttpRequestException("Budget for the given month already exists.");
            }

            decimal totalAmount = await transactionRepo.GetTotalSpendingAsync(userId, categoryId, budget.Month, null);
           
            if(totalAmount > limitAmount)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AllowTransaction(Transaction transaction, Guid userId)
        {
           var categoryId = transaction.CategoryId;
           var amount = transaction.Amount;

           if (!await categoryRepo.AnyAsync(c => c.Id == categoryId && c.UserId == userId))
            {
                throw new KeyNotFoundException("Given category does not exist.");
            }

           var budget = await budgetRepo.GetByCategoryAsync(categoryId);

           if (budget is null) return true;

           var month = budget.Month;

           decimal totalAmount = amount + await transactionRepo.GetTotalSpendingAsync(userId, categoryId, month, transaction.Id);
           
           if (transaction.Type == TransactionType.Expense && totalAmount > budget.LimitAmount) return false;

           return true;
        }
    }
}