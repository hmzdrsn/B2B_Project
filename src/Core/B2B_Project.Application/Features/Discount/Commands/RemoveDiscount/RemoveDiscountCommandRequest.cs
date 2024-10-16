using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Discount.Commands.RemoveDiscount
{
    public class RemoveDiscountCommandRequest : IRequest<HandlerResponse<RemoveDiscountCommandResponse>>
    {
        public string DiscountId { get; set; } = default!;
    }


}
