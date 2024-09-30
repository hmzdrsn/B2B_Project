using B2B_Project.Domain.Common;
using B2B_Project.Domain.Identity;

namespace B2B_Project.Domain.Entities
{
    public class Address : BaseEntity
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
        public AppUser AppUser { get; set; } = default!;
    }
}
