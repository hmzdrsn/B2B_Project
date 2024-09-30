using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Message.Queries.GetMessagesByDefaultFilter
{
    public class GetMessagesByDefaultFilterQueryRequest : IRequest<HandlerResponse<List<GetMessagesByDefaultFilterQueryResponse>>>
    {
        public string Username { get; set; } = default!;
        public string receiverId { get; set; } = default!;
        public int Size { get; set; }
        public int CurrentOrder { get; set; }
    }
}
