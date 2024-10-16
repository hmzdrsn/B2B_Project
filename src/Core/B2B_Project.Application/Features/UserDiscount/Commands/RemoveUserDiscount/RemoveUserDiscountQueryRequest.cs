
using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.UserDiscount.Commands.RemoveUserDiscount
{
    public class RemoveUserDiscountQueryRequest : IRequest<HandlerResponse<RemoveUserDiscountQueryResponse>>
    {
        public string UserDiscountId { get; set; } = default!;
    }
}
