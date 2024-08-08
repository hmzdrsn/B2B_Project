using B2B_Project.Application.Features.Product.Commands.CreateProduct;
using B2B_Project.Application.Features.Product.Queries;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace B2B_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //BASE CONTROLLER YAPILACAK
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            GetAllProductQueryRequest request = new GetAllProductQueryRequest();
            var res = _mediator.Send(request);
            return Ok(res.Result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateProductCommandRequest req)
        {
            var res =_mediator.Send(req);
            return Ok(res.Result);
        }
    }
}
