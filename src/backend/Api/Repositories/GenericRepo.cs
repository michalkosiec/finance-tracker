using System.Linq.Expressions;
using Api.Data;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public abstract class GenericRepo<T>(AppDbContext context) : IGenericRepo<T> where T : class
    {
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetFirstOrDefaultByAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().AnyAsync(predicate);
        }

        public async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await SaveChanges();
            }
        }
        
        protected async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }
    }
}