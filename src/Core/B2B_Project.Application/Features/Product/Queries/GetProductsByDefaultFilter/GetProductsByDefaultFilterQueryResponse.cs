namespace B2B_Project.Application.Features.Product.Queries.GetDefaultProductsByFilter
{
    public class GetProductsByDefaultFilterQueryResponse
    {
        public string ProductId { get; set; }
        public string ProductUrl { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}
