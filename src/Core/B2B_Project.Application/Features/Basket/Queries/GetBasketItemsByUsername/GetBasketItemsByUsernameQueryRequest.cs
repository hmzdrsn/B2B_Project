using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Basket.Queries.GetBasketItemsByUsername
{
    public class GetBasketItemsByUsernameQueryRequest : IRequest<HandlerResponse<GetBasketItemsByUsernameQueryResponse>>
    {
        public string Username { get; set; } = default!;
    }

}
