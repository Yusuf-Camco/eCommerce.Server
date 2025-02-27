using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Products;
using eCommerce.Application.Exceptions;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Application.Services.Implementations
{
    public class ProductService(IGeneric<Product> product, IMapper mapper) : IProductService
    {
        async Task<ServiceResponse> IProductService.AddAsync(CreateProduct entity)
        {
            var mappedData = mapper.Map<Product>(entity);
            int result = await product.AddAsync(mappedData);
            return result > 0 ? new ServiceResponse(true, "Product added successfully") : new ServiceResponse(false, "Product failed to be added!");
        }

        async Task<ServiceResponse> IProductService.DeleteAsync(Guid id)
        {
            int result = await product.DeleteAsync(id);
            return result <= 0 ? new ServiceResponse(false, "Product not found or failed to be deleted") : 
                new ServiceResponse(true, "Product was deleted successfully");            
        }

        async Task<IEnumerable<GetProduct>> IProductService.GetAllAsync()
        {
            var rawData = await product.GetAllAsync();
            if(!rawData.Any()) return [];
            return mapper.Map<IEnumerable<GetProduct>>(rawData);

        }

        async Task<GetProduct> IProductService.GetByIdAsync(Guid id)
        {
            var rawData = await product.GetByIdAsync(id);
            if (rawData == null) return new GetProduct();
            return mapper.Map<GetProduct>(rawData); 
        }

        async Task<ServiceResponse> IProductService.UpdateAsync(UpdateProduct entity)
        {
            var mappedData = mapper.Map<Product>(entity);
            var result = await product.UpdateAsync(mappedData);
            return result != null ? new ServiceResponse(true, "Product deleted successfully") : new ServiceResponse(false, "Product failed to delete");
        }
    }
}
