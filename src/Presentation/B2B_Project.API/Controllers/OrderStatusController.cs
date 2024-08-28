using B2B_Project.Application.Features.OrderStatus.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetAllOrderStatusQueryRequest req = new();
            var res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}
