using B2B_Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Features.Order.Queries.GetOrderWithDetailsById
{
    public class GetOrderWithDetailsByIdQueryResponse
    {
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; }
    }

    public sealed class OrderDetailResponse
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
