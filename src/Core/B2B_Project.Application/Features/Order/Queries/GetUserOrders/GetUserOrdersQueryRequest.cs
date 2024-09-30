using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Order.Queries.GetUserOrders
{
    public class GetUserOrdersQueryRequest : IRequest<HandlerResponse<List<GetUserOrdersQueryResponse>>>
    {
        public string Username { get; set; } = default!;
    }
}
