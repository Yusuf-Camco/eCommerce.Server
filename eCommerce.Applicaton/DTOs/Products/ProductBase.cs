namespace eCommerce.Application.DTOs.Products
{
    public class CategoryBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
    }
}
