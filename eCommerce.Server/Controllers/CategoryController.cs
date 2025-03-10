using eCommerce.Application.DTOs.Category;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService service) : ControllerBase
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
        public async Task<IActionResult> Add(CreateCategory entity)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await service.AddAsync(entity);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCategory entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await service.UpdateAsync(entity);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await service.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
