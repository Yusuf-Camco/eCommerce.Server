using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Products;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Mapping
{
    public class MappingConfig:Profile
    {

        public MappingConfig()
        {
            //CreateMap<CreateCategory, Category>();    
            //CreateMap<CreateProduct, Product>();


            // CreateMap<Category, GetCategory>();
            // CreateMap<Product, GetProduct>();

            // Category Mappings
            CreateMap<CreateCategory, Category>().ReverseMap();
            CreateMap<Category, GetCategory>().ReverseMap();

            // Product Mappings
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<Product, GetProduct>().ReverseMap();

            CreateMap<UpdateCategory, Category>().ReverseMap();
        }
    }
}
