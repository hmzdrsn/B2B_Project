using B2B_Project.Application.Common.Models;
using MediatR;
using Newtonsoft.Json;

namespace B2B_Project.Application.Features.Basket.Queries
{
    public class GetBasketByUsernameQueryRequest : IRequest<HandlerResponse< GetBasketByUsernameQueryResponse>>
    {
        [JsonIgnore]
        public string? Username { get; set; }
    }
}
