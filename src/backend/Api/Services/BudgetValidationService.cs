using Api.Data;
using Api.Dtos.Transactions;
using Api.Models;
using Api.Repositories.Interfaces;

namespace Api.Services
{
    public class BudgetValidationService(IBudgetRepo budgetRepo, ITransactionRepo transactionRepo) : IBudgetValidationService
    {
        public async Task<bool> AllowTransaction(Transaction transaction, Guid userId)
        {
           var categoryId = transaction.CategoryId;
           var amount = transaction.Amount;

           var budget = await budgetRepo.GetByCategoryAsync(categoryId);
           var month = budget!.Month;

           decimal totalAmount = amount + await transactionRepo.GetTotalSpendingAsync(userId, categoryId, month);
           
           if (transaction.Type == TransactionType.Expense && totalAmount > budget.LimitAmount) return false;

           return true;
        }
    }
}