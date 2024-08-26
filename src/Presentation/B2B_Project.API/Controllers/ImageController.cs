using B2B_Project.Application.Features.Image.Commands.RemoveImage;
using B2B_Project.Application.Features.Product.Commands.RemoveProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById([FromQuery] RemoveImageCommandRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}
