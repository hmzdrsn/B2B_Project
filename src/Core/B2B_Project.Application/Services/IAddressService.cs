using B2B_Project.Application.Features.Address.Commands.SetAddressStatus;
using B2B_Project.Application.Features.Address.Queries.GetUserAddress;

namespace B2B_Project.Application.Services
{
    public interface IAddressService
    {
        Task<GetUserAddressesQueryResponse> GetUserAddresses(GetUserAddressesQueryRequest request);
        Task<bool> SetAddressStatus(SetAddressStatusCommandRequest request);
    }
}
