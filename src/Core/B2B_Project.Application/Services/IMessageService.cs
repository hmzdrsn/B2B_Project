using B2B_Project.Application.Features.Message.Queries.GetMessagesByDefaultFilter;

namespace B2B_Project.Application.Services
{
    public interface IMessageService
    {
        Task<List<GetMessagesByDefaultFilterQueryResponse>> GetMessagesByDefaultFilter(GetMessagesByDefaultFilterQueryRequest request);
    }
}
