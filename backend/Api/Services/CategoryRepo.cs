using Api.Data;
using Api.Models;

namespace Api.Services
{
    public class CategoryRepo(AppDbContext context) : GenericRepo<Category>(context), ICategoryRepo
    {
    }

}