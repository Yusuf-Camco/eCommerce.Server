using eCommerce.Application.Exceptions;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces.CategorySpecifics;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.CategorySpecifics
{
    public class CategoryRepo(AppDbContext context) : ICategory
    {
        public async Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId)
        {
            var products = await context
                .Products.Include(p => p.Categories)
                .Where(p => p.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();

            return products.Count > 0 ? products : [];
        }
    }
}