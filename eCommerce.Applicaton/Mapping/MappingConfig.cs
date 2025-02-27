using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Products;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Mapping
{
    public class MappingConfig:Profile
    {

        public MappingConfig()
        {
           CreateMap<CreateCategory, Category>();    
           CreateMap<CreateProduct, Product>();


            CreateMap<Category, GetCategory>();
            CreateMap<Product, GetProduct>();
        }
    }
}
