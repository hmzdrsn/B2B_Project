using B2B_Project.Application.Features.Message.Commands.SendMessage;
using B2B_Project.Application.Features.Message.Queries.GetMessagesByDefaultFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetMessagesByDefaultFilter(int size, int currentOrder, string receiverId)
        {
            GetMessagesByDefaultFilterQueryRequest req = new();
            req.Username = User.FindFirstValue(ClaimTypes.Name);
            req.Size = size;
            req.CurrentOrder = currentOrder;
            req.receiverId = receiverId;
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> SendMessage(string receiverId, string content)
        {
            SendMessageCommandRequest req = new();
            req.receiverId = receiverId;
            req.content = content;
            req.senderUsername = User.FindFirstValue(ClaimTypes.Name);
            var res = await _mediator.Send(req);
            return Ok(res);
        }

    }
}
