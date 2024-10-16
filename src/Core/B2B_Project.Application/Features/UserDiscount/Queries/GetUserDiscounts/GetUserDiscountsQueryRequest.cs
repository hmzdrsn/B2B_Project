
using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.UserDiscount.Queries.GetUserDiscounts
{
    public class GetUserDiscountsQueryRequest : IRequest<HandlerResponse<List<GetUserDiscountsQueryResponse>>>
    {
        public string Username { get; set; } = default!;
    }
}
