using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Products;

namespace eCommerce.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateCategory entity);
        Task<ServiceResponse> UpdateAsync(UpdateCategory entity);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId);
    }
}
