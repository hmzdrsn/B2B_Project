using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Queries
{
    public class GetBasketByUsernameQueryRequest : IRequest<HandlerResponse<GetBasketByUsernameQueryResponse>>
    {
        public string? Username { get; set; }
    }
}
