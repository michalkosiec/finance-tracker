using Api.Data;
using Api.Models;

namespace Api.Services
{
    public class BudgetRepo(AppDbContext context) : GenericRepo<Budget>(context), IBudgetRepo
    {
      
    }
}