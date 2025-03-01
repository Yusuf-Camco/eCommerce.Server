using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Products;
using eCommerce.Application.Exceptions;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Application.Services.Implementations
{
    public class ProductService(IGeneric<Product> product, IMapper mapper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProduct entity)
        {
            // CreateMap<CreateProduct, Product>().ReverseMap();
            var mappedData = mapper.Map<Product>(entity);
            int result = await product.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product added successfully") : new ServiceResponse(false, "Product failed to be added!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await product.DeleteAsync(id);
            return result <= 0 ? new ServiceResponse(false, "Product not found or failed to be deleted") : 
                new ServiceResponse(true, "Product was deleted successfully");            
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var rawData = await product.GetAllAsync();
            if(!rawData.Any()) return [];
            return mapper.Map<IEnumerable<GetProduct>>(rawData);

        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var rawData = await product.GetByIdAsync(id);
            if (rawData == null) return new GetProduct();
            return mapper.Map<GetProduct>(rawData); 
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct entity)
        {
            var existingCategory = await product.GetByIdAsync(entity.Id);
            if (existingCategory == null)
            {
                return new ServiceResponse(false, "Category not found");
            }


            var mappedData = mapper.Map(entity, existingCategory);  // Map changes to existing entity

            var result = await product.UpdateAsync(mappedData);
            return result != null ? new ServiceResponse(true, "Category updated successfully")
                                  : new ServiceResponse(false, "Category failed to update");
        }
    }
}
