using B2B_Project.Application.Features.Product.Queries.GetAllProduct;
using B2B_Project.Application.Features.Product.Queries.GetCompanyProductsByUsername;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2B_Project.API.Controllers.Company
{
    [Route("api/Company/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetCompanyProductsByUsernameQueryRequest req)
        {
            var data = await _mediator.Send(req);
            return Ok(data);
        }
    }
}
