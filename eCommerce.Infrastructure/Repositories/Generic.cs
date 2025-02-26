using eCommerce.Application.Exceptions;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var entity = await context.Set<T>().FindAsync(id) ?? 
                throw new ItemNotFound($"item with {id} is not found");
            context.Set<T>().Remove(entity);
            return await context.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
