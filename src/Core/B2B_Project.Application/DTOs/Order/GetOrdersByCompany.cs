namespace B2B_Project.Application.DTOs.Order
{
    public class GetOrdersByCompany
    {
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly OrderDate{ get; set; }
    }
}
