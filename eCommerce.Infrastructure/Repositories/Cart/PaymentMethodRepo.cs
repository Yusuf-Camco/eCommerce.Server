using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Cart
{
    public class PaymentMethodRepo(AppDbContext context) : IPaymentMethod
    {
        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethods()
        {
            return await context.PaymentMethods.AsNoTracking().ToListAsync();
        }
    }
}
