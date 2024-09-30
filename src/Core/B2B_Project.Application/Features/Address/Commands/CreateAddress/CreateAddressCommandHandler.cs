using B2B_Project.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Address.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommandRequest, HandlerResponse<CreateAddressCommandResponse>>
    {
        private readonly IAddressWriteRepository _addressWriteRepository;
        private readonly IAddressReadRepository _addressReadRepository;
        public CreateAddressCommandHandler(IAddressWriteRepository addressWriteRepository, IAddressReadRepository addressReadRepository)
        {
            _addressWriteRepository = addressWriteRepository;
            _addressReadRepository = addressReadRepository;
        }

        public async Task<HandlerResponse<CreateAddressCommandResponse>> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Address address = new();
            address.Title = request.Title;
            address.Phone = request.Phone;
            address.FullName = request.FullName;
            address.Address1 = request.Address1;
            address.Address2 = request.Address2;
            address.State = request.State;
            address.City = request.City;
            address.Country = request.Country;
            address.PostalCode = request.PostalCode;
            address.AppUserId = request.AppUserId;
            //user kontrolü gerekli
            address.IsActive = request.IsActive;
            if (request.IsActive)
            {
                var activeAddresses = await _addressReadRepository.Table
                    .Where(x => x.IsActive == true)
                    .ToListAsync();
                if (activeAddresses != null)
                {
                    foreach (var item in activeAddresses)
                    {
                        item.IsActive = false;
                        _addressWriteRepository.Update(item);
                    }
                }
            }
            var res = await _addressWriteRepository.AddAsync(address);
            var count = await _addressWriteRepository.SaveAsync();
            if (count > 0 && res)
            {
                return new()
                {
                    Message = "Address Created"
                };
            }
            return new()
            {
                Message = "An Error Occurred While Address Creating!"
            };
        }
    }
}
