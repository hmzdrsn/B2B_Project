using B2B_Project.Application;
using B2B_Project.Application.Features.Message.Commands.SendMessage;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace B2B_Project.Persistance.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly IMessageWriteRepository _messageWriteRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatHubService(IMessageWriteRepository messageWriteRepository, UserManager<AppUser> userManager, IHubContext<ChatHub> hubContext)
        {
            _messageWriteRepository = messageWriteRepository;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        public async Task<bool> SendMessageAsync(SendMessageCommandRequest request)
        {
            var senderUser = await _userManager.FindByNameAsync(request.senderUsername);
            if (senderUser == null)
            {
                return false;
            }

            bool result = await _messageWriteRepository.AddAsync(new()
            {
                Content = request.content,
                ReceiverId = request.receiverId,
                SenderId = senderUser.Id,
            });
            var count = await _messageWriteRepository.SaveAsync();
            if (result && count > 0)
            {
                await _hubContext.Clients.Group(request.receiverId).SendAsync("receiveMessage", new
                {
                    content = request.content,
                    receiverId = request.receiverId,
                    senderId = senderUser.Id
                });
                //await _hubContext.Clients.All.SendAsync("receiveMessage", "Herkes");
                return true;
            }
            return false;
        }



    }
}
