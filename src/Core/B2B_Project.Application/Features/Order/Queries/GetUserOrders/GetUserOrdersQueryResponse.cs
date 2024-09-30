namespace B2B_Project.Application.Features.Order.Queries.GetUserOrders
{
    public class GetUserOrdersQueryResponse
    {
        public string AppUserId { get; set; } = default!;
        public decimal TotalPrice { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } = default!;
    }
}
