using B2B_Project.Application;
using B2B_Project.Application.Features.Address.Commands.SetAddressStatus;
using B2B_Project.Application.Features.Address.Queries.GetUserAddress;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Services
{
    public class AddressService : IAddressService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAddressReadRepository _addressReadRepository;
        private readonly IAddressWriteRepository _addressWriteRepository;

        public AddressService(UserManager<AppUser> userManager, IAddressReadRepository addressReadRepository, IAddressWriteRepository addressWriteRepository)
        {
            _userManager = userManager;
            _addressReadRepository = addressReadRepository;
            _addressWriteRepository = addressWriteRepository;
        }

        public async Task<GetUserAddressesQueryResponse> GetUserAddresses(GetUserAddressesQueryRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user is null)
            {
                return null;
            }
            var res = await _addressReadRepository.Table
                        .Where(x => x.AppUserId == user.Id)
                        .Select(x => new GetUserAddressQueryResponse()
                        {
                            Title = x.Title,
                            FullName = x.FullName,
                            Phone = x.Phone,
                            AddressId = x.Id.ToString(),
                            Address1 = x.Address1,
                            Address2 = x.Address2,
                            AppUserId = user.Id,
                            City = x.City,
                            Country = x.Country,
                            IsActive = x.IsActive,
                            PostalCode = x.PostalCode,
                            State = x.State
                        })
                        .ToListAsync();
            if (res is not null)
            {
                return new()
                {
                    Addresses = res
                };
            }
            return null;
        }

        public async Task<bool> SetAddressStatus(SetAddressStatusCommandRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user is null)
            {
                return false;
            }
            var address = await _addressReadRepository.GetByIdAsync(request.AddressId);

            if (address is null)
            {
                return false;
            }
            var activeAddresses = _addressReadRepository.Table.Where(x => x.IsActive == true && x.AppUserId == user.Id);
            if (activeAddresses is not null)
            {
                foreach (var item in activeAddresses)
                {
                    item.IsActive = false;
                    _addressWriteRepository.Update(item);
                }
            }
            if (!address.IsActive)
            {
                address.IsActive = true;
            }
            var res = _addressWriteRepository.Update(address);
            var count = await _addressWriteRepository.SaveAsync();
            if (res && count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
