using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Address.Commands.SetAddressStatus
{
    public class SetAddressStatusHandler : IRequestHandler<SetAddressStatusCommandRequest, HandlerResponse<SetAddressStatusCommandResponse>>
    {
        private readonly IAddressService _addressService;

        public SetAddressStatusHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<HandlerResponse<SetAddressStatusCommandResponse>> Handle(SetAddressStatusCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _addressService.SetAddressStatus(request))
            {
                return new()
                {
                    Message = "Address Activated Successfully!"
                };
            }
            return new()
            {
                Message = "An Error Occurred While Activating Address!"
            };
        }
    }
}
