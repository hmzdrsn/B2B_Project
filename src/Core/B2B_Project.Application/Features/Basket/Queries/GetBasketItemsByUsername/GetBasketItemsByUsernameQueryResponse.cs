namespace B2B_Project.Application.Features.Basket.Queries.GetBasketItemsByUsername
{
    public class GetBasketItemsByUsernameQueryResponse
    {
        public List<BasketItemProduct>? Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class BasketItemProduct
    {
        public string ProductId { get; set; } = default!;
        public string ProductImageUrl { get; set; } = string.Empty;
        public string ProductName { get; set; } = default!;
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
