using B2B_Project.Domain.Common;
using B2B_Project.Domain.Identity;

namespace B2B_Project.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string SenderId { get; set; } = default!;
        public AppUser Sender { get; set; } = default!;
        public string ReceiverId { get; set; } = default!;
        public AppUser Receiver { get; set; } = default!;
        public string Content { get; set; } = default!;
        //IsRead Okundu Bilgisi eklenebilir...
    }
}
