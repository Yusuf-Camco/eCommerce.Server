using eCommerce.Application.DTOs.Products;

namespace eCommerce.Application.DTOs.Category
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; }
    }
}
