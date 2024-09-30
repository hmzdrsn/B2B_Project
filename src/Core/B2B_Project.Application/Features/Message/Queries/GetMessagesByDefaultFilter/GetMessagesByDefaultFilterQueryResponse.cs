namespace B2B_Project.Application.Features.Message.Queries.GetMessagesByDefaultFilter
{
    public class GetMessagesByDefaultFilterQueryResponse
    {
        public string senderId { get; set; } = default!;
        public string receiverId { get; set; } = default!;
        public string content { get; set; } = default!;
        public DateTime? date { get; set; } = default!;
    }
}
