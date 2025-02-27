using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> category, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory entity)
        {
            var mappedData = mapper.Map<Category>(entity);
            int result = await category.AddAsync(mappedData);
            return new ServiceResponse(true, "category added successfully");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await category.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "category deleted successfully") : new ServiceResponse(false, "category failed to delete");
        }

        public async Task<IEnumerable<CreateCategory>> GetAllAsync()
        {
            var rawData = await category.GetAllAsync();
            if (!rawData.Any()) return [];
            return mapper.Map<IEnumerable<CreateCategory>>(rawData);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await category.GetByIdAsync(id);
            if (rawData == null) return new GetCategory();
            return mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory entity)
        {
            var mappedData = mapper.Map<Category>(entity);
            var result = await category.UpdateAsync(mappedData);
            return result != null ? new ServiceResponse(true, "category deleted successfully") : new ServiceResponse(false, "category failed to delete");
        }
    }
}
