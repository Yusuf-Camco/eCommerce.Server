using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Products;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Mapping
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            // Category Mappings
            CreateMap<CreateCategory, Category>().ReverseMap();
            CreateMap<Category, GetCategory>().ReverseMap();
            CreateMap<UpdateCategory, Category>().ReverseMap();

            // Product Mappings
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<Product, GetProduct>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();
        }
    }
}
