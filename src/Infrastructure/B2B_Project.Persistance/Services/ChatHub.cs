using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace B2B_Project.Persistance.Services
{
    public class ChatHub : Hub
    {
        UserManager<AppUser> _userManager;

        public ChatHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Connect(string userName)
        {


        }
        public override async Task OnConnectedAsync()
        {
            var userName = Context.GetHttpContext().Request.Query["access_token"];
            var user = await _userManager.FindByNameAsync(userName);
            user.IsOnline = true;
            await _userManager.UpdateAsync(user);
            await Groups.AddToGroupAsync(Context.ConnectionId, user.Id);//user.Id.ToString()

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.GetHttpContext().Request.Query["access_token"];
            var user = await _userManager.FindByNameAsync(userName);
            user.IsOnline = false;
            await _userManager.UpdateAsync(user);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.Id.ToString());
            await base.OnDisconnectedAsync(exception);
        }
    }
}
