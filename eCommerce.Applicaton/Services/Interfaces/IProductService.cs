using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProduct>> GetAllAsync();
        Task<GetProduct> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateProduct entity);
        Task<ServiceResponse> UpdateAsync(UpdateProduct entity);
        Task<ServiceResponse> DeleteAsync(Guid id);
    } 
}
