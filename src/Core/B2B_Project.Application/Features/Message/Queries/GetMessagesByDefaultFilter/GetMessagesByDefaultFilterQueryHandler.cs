using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Message.Queries.GetMessagesByDefaultFilter
{
    public class GetMessagesByDefaultFilterQueryHandler : IRequestHandler<GetMessagesByDefaultFilterQueryRequest, HandlerResponse<List<GetMessagesByDefaultFilterQueryResponse>>>

    {
        private readonly IMessageService _messageService;

        public GetMessagesByDefaultFilterQueryHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<HandlerResponse<List<GetMessagesByDefaultFilterQueryResponse>>> Handle(GetMessagesByDefaultFilterQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _messageService.GetMessagesByDefaultFilter(request);
            if (res == null)
            {
                return new()
                {
                    Message = "An Error Ocurred!"
                };
            }
            return new()
            {
                Data = res
            };
        }
    }
}
