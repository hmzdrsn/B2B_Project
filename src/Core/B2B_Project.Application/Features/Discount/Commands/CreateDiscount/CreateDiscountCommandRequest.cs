using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Discount.Commands.CreateDiscount
{
    public class CreateDiscountCommandRequest : IRequest<HandlerResponse<CreateDiscountCommandResponse>>
    {
        public string DiscountCode { get; set; } = default!;
        public double DiscountAmount { get; set; }
        public bool isPercentage { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public int MaxUsagePerUser { get; set; }
        public string Username { get; set; } = default!;
    }

}
