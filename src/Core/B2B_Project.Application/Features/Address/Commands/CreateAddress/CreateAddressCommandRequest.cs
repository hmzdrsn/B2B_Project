using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Address.Commands.CreateAddress
{
    public class CreateAddressCommandRequest : IRequest<HandlerResponse<CreateAddressCommandResponse>>
    {
        public string Title { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Address1 { get; set; } = default!;
        public string? Address2 { get; set; }
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string AppUserId { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
