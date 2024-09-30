namespace B2B_Project.Application.Features.Address.Queries.GetUserAddress
{
    public class GetUserAddressesQueryResponse
    {
        public List<GetUserAddressQueryResponse>? Addresses { get; set; }
    }
    public class GetUserAddressQueryResponse
    {
        public string Title { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string AddressId { get; set; } = default!;
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
