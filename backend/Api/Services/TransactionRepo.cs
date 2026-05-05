using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class TransactionRepo(AppDbContext context) : GenericRepo<Transaction>(context), ITransactionRepo
    {
    }

}