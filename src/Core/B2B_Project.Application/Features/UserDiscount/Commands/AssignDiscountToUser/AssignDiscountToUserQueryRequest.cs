using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.UserDiscount.Commands.AssignDiscountToUser
{
    public class AssignDiscountToUserQueryRequest : IRequest<HandlerResponse<AssignDiscountToUserQueryResponse>>
    {
        public string Username { get; set; } = default!;
        public string UsernameTo { get; set; } = default!;
        public string DiscountId { get; set; } = default!;

    }
}
