using eCommerce.Application.DTOs.Products;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService service) : ControllerBase
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllAsync();
            return result.Count() > 0 ? Ok(result) : NotFound(result);
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await service.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound(result) ;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateProduct entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await service.AddAsync(entity);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProduct entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await service.UpdateAsync(entity);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await service.DeleteAsync(id);
            return result != null ? Ok(result) : BadRequest(result);
        }
    }
}
