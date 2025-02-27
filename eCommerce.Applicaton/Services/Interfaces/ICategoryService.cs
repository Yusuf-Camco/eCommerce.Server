using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;

namespace eCommerce.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CreateCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateCategory entity);
        Task<ServiceResponse> UpdateAsync(UpdateCategory entity);
        Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
