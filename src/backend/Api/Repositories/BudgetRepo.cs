using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class BudgetRepo(AppDbContext context) : UserOwnedRepo<Budget>(context), IBudgetRepo {}
}