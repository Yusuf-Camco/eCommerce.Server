using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using Stripe.Checkout;

namespace eCommerce.Infrastructure.Repostiories.Cart
{
    public class StripePaymentService : IPaymentService
    {
        public Task<ServiceResponse> MakePayment(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts)
        {
            try
            {
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var product in cartProducts)
                {
                    var pQuantity = carts.FirstOrDefault(c => c.ProductId == product.Id)?.Quantity;
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = product.Name,
                            },
                            UnitAmount = (long)product.Price * 100,
                        },
                        Quantity = pQuantity,
                    });
                }
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https://example.com/success",
                    CancelUrl = "https://example.com/cancel",
                };
                var service = new SessionService();
                var session = service.CreateAsync(options);
                return Task.FromResult(new ServiceResponse  
                {
                    Success = true,
                    Message = "Payment successful"
                    //Data = session
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ServiceResponse(false, ex.Message));
            }


        }
    }
}
