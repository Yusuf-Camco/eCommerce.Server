using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Cart;

namespace eCommerce.Application.Services.Implementations.Cart
{
    public class CartService(ICart cart, IMapper mapper, IGeneric<Product> productInterface, IPaymentMethodServices paymentMethodService, IPaymentService paymentService) : ICartService
    {
        public async Task<ServiceResponse> CheckOutHistory(Checkout checkouts)
        {
            var (products, totalAmount) = await GetTotalAmount(checkouts.Carts);
            var paymentMethods = await paymentMethodService.GetpaymentMethods();

            if (checkouts.PayementMethodId == paymentMethods.FirstOrDefault()!.Id)
                return await paymentService.MakePayment(totalAmount, products, checkouts.Carts);
            else
                return new ServiceResponse { Success = false, Message = "Invalid Payment Method" };
        }

        public async Task<ServiceResponse> SaveCheckOutHistory(IEnumerable<CreateArchive> newArchive)
        {
            var mappedData = mapper.Map<IEnumerable<Archive>>(newArchive);
            var result = await cart.SaveCheckOutHistory(mappedData);
            return result > 0 ? new ServiceResponse { Success = true, Message = "CheckOut History Saved Successfully" } : new ServiceResponse { Success = false, Message = "Failed to Save CheckOut History" };
        }

        private async Task<(IEnumerable<Product>, decimal)> GetTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if (carts == null || !carts.Any())
            {
                return ([], 0);
            }

            var products = await productInterface.GetAllAsync();
            if (!products.Any()) return ([], 0);

            var cartedProducts = carts
                .Select(cartItem => products.FirstOrDefault(products => products.Id == cartItem.ProductId))
                .Where(p => p != null)
                .ToList();

            var totalAmount = carts
                .Where(cartItem => cartedProducts
                .Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartedProducts
                .First(p => p!.Id == cartItem.ProductId)!.Price);

            return (cartedProducts!, totalAmount);

        }
    }
}
