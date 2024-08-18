﻿using B2B_Project.Domain.Common;
using B2B_Project.Domain.Identity;

namespace B2B_Project.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        //eklenirse indiirm oranı burada olabilir.
        public Guid? OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
