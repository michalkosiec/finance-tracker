using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;

namespace Api.Repositories
{
    public class CategoryRepo(AppDbContext context) : UserOwnedRepo<Category>(context), ICategoryRepo {}
}