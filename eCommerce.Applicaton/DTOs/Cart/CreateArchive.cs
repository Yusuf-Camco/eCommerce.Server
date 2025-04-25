using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart
{
    public class CreateArchive
    {
        [Required(ErrorMessage = "User Id is required")]
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Product Id is required")]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
