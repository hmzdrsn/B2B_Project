namespace B2B_Project.Application.DTOs.Order
{
    public class GetOrdersByCompany
    {
        public string OrderId { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly OrderDate{ get; set; }
        public string OrderStatus { get; set; }
    }
}
