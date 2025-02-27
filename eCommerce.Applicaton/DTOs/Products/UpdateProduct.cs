using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Products
{
    public class UpdateProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        
    }
}
