using eCommerce.Application.Exceptions;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    public class Generic<T>(AppDbContext context) : IGeneric<T> where T : class
    {
        public async Task<int> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return await context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
                return 0;

            context.Set<T>().Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().AsNoTracking<T>().ToListAsync();


        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id) ??
                throw new ItemNotFound($"item with {id} was not found");
            return entity!;
        }

        public async Task<T> UpdateAsync(T entity) => await context.SaveChangesAsync() > 0 ? context.Set<T>().Update(entity).Entity : throw new Exception("Failed to update entity");
    }
}

