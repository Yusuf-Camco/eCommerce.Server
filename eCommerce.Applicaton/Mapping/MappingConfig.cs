using AutoMapper;
using eCommerce.Application.DTOs.Authentication;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.DTOs.Products;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Identity;

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

            //User mapping
            CreateMap<AppUser, CreateUser>().ReverseMap();
            CreateMap<AppUser, LoginUser>().ReverseMap();
            CreateMap<GetUser, AppUser>().ReverseMap();
        }
    }
}
