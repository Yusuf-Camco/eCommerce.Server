using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentMethodServices paymentMethodService) : ControllerBase
    {
        [HttpGet("getPaymentMethods")]
        public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
        {
            var result = await paymentMethodService.GetpaymentMethods();
            return result.Count() > 0 ? Ok(result) : NotFound(result);
        }
    }
}
