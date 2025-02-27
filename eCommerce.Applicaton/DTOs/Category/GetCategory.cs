using eCommerce.Application.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Category
{
    public class GetCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public ICollection<GetProduct>? Products { get; set; }
    }
}
