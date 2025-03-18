using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService _cartService) : ControllerBase
    {

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOutHistory(Checkout checkouts)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _cartService.CheckOutHistory(checkouts);
            return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpPost("save-checkout")]
        public async Task<IActionResult> SaveCheckOutHistory(IEnumerable<CreateArchive> newArchives)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _cartService.SaveCheckOutHistory(newArchives);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
