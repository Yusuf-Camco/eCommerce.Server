using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;

namespace eCommerce.Infrastructure.Repositories.Cart
{
    public class Cart(AppDbContext context) : ICart
    {
        public async Task<int> SaveCheckOutHistory(IEnumerable<Archive> checkouts)
        {
            context.CheckOutArchives.AddRange(checkouts);
            return await context.SaveChangesAsync();
        }
    }
}
