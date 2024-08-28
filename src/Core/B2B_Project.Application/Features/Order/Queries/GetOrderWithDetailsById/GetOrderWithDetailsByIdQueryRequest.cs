using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Order.Queries.GetOrderWithDetailsById
{
    public class GetOrderWithDetailsByIdQueryRequest : IRequest<HandlerResponse<GetOrderWithDetailsByIdQueryResponse>>
    {
        public string OrderId { get; set; } = default!;
    }
}
