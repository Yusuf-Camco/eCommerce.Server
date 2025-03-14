using eCommerce.Application.DTOs.Authentication;
using eCommerce.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthenticationService authService) : ControllerBase()
    {
        
        [HttpPost("addUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser user)
        {
            var result = await authService.CreateUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await authService.GetAllUsers();
            return result.Count() > 0 ? Ok(result) : NotFound(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUser user)
        {
            var result = await authService.LoginUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("reviveToken")]
        public async Task<IActionResult> ReviveToken([FromBody] ReviveTokenRequest request)
        {
            var result = await authService.ReviveToken(request.RefreshToken!);
            return Ok(result);
        }
    }
}
