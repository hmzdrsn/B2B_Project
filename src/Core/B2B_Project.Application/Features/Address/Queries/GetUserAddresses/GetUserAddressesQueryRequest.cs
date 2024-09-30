using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Address.Queries.GetUserAddress
{
    public class GetUserAddressesQueryRequest : IRequest<HandlerResponse<GetUserAddressesQueryResponse>>
    {
        public string Username { get; set; } = default!;
    }
}
