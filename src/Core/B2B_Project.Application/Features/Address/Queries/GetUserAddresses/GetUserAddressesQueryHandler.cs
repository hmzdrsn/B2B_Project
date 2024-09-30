using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Address.Queries.GetUserAddress
{
    public class GetUserAddressQueryHandler : IRequestHandler<GetUserAddressesQueryRequest, HandlerResponse<GetUserAddressesQueryResponse>>
    {
        private readonly IAddressService _addressService;

        public GetUserAddressQueryHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<HandlerResponse<GetUserAddressesQueryResponse>> Handle(GetUserAddressesQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _addressService.GetUserAddresses(request);
            if (res is not null)
            {
                return new()
                {
                    Data = res
                };
            }

            return new()
            {
                Message = "An Error Ocurred While Listing Addresses!"
            };
        }
    }
}
