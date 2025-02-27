using eCommerce.Application.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Category
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }        
    }
}
