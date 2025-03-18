using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Products;
using eCommerce.Application.Exceptions;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.CategorySpecifics;

namespace eCommerce.Application.Services.Implementations
{
    public class CategoryService(IGeneric<Category> category, IMapper mapper, ICategory cat) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory entity)
        {
            var mappedData = mapper.Map<Category>(entity);
            _ = await category.AddAsync(mappedData);
            return new ServiceResponse(true, "category added successfully");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await category.DeleteAsync(id);

            return result <= 0 ? new ServiceResponse(false, "category not found or failed to be deleted") :
                new ServiceResponse(true, "category was deleted successfully");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await category.GetAllAsync();
            if (!rawData.Any()) return [];
            return mapper.Map<IEnumerable<GetCategory>>(rawData);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await category.GetByIdAsync(id);
            if (rawData == null) return new GetCategory();
            return mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory entity)
        {
            var existingCategory = await category.GetByIdAsync(entity.Id);
            if (existingCategory == null)
            {
                return new ServiceResponse(false, "Category not found");
            }


            var mappedData = mapper.Map(entity, existingCategory);  // Map changes to existing entity

            var result = await category.UpdateAsync(mappedData);
            return result != null ? new ServiceResponse(true, "Category updated successfully")
                                  : new ServiceResponse(false, "Category failed to update");         
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products = await cat.GetProductsByCategory(categoryId);
            if (!products.Any()) return [];
            return mapper.Map<IEnumerable<GetProduct>>(products);
        }
    }
}
