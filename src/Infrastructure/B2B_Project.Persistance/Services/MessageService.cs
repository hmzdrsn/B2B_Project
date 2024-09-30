using B2B_Project.Application;
using B2B_Project.Application.Features.Message.Queries.GetMessagesByDefaultFilter;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageReadRepository _messageReadRepository;
        private readonly UserManager<AppUser> _userManager;

        public MessageService(IMessageReadRepository messageReadRepository, UserManager<AppUser> userManager)
        {
            _messageReadRepository = messageReadRepository;
            _userManager = userManager;
        }

        public async Task<List<GetMessagesByDefaultFilterQueryResponse>> GetMessagesByDefaultFilter(GetMessagesByDefaultFilterQueryRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return null;
            }

            var messages = await _messageReadRepository.Table
                .Where(x => (x.SenderId == user.Id && x.ReceiverId == request.receiverId) || (x.SenderId == request.receiverId && x.ReceiverId == user.Id))
                .OrderByDescending(x => x.CreatedDate)
                .Skip((request.CurrentOrder - 1) * request.Size)
                .Take(request.Size)
                .Select(x => new GetMessagesByDefaultFilterQueryResponse()
                {
                    senderId = x.SenderId,
                    receiverId = x.ReceiverId,
                    content = x.Content,
                    date = x.CreatedDate
                })
                .ToListAsync();
            return messages;
        }
    }
}
