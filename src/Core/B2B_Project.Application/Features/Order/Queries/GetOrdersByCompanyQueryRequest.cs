using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Order.Queries
{
    public class GetOrdersByCompanyQueryRequest : IRequest<HandlerResponse<GetOrdersByCompanyQueryResponse>>
    {
        public string Username { get; set; }
    }

}
