using B2B_Project.Application.Features.Product.Commands.CreateProduct;
using B2B_Project.Application.Features.Product.Queries.GetAllProduct;
using B2B_Project.Application.Features.Product.Queries.GetCompanyProductsByUsername;
using B2B_Project.Application.Features.Product.Queries.GetProductsByCategory;
using B2B_Project.Application.Features.Product.Queries.GetProductsByCompany;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetAllProductQueryRequest request = new GetAllProductQueryRequest();
            var res =await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyProducts([FromQuery] GetCompanyProductsByUsernameQueryRequest req)
        {
            var data = await _mediator.Send(req);
            return Ok(data);
        }
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest req)
        {
            var res =_mediator.Send(req);
            return Ok(res.Result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCompany([FromQuery] GetProductsByCompanyQueryRequest req)
        {
            var res =await _mediator.Send(req);
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory([FromQuery] GetProductsByCategoryQueryRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}
