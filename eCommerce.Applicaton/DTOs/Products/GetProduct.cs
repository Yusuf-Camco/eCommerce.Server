using eCommerce.Application.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Products
{
    public class GetProduct : ProductBase
    {       
        public Guid Id { get; set; }

        public GetCategory? Category { get; set; }
    }
}
