﻿using B2B_Project.Application.Features.Product.Commands.CreateProduct;
using B2B_Project.Application.Features.Product.Commands.RemoveProduct;
using B2B_Project.Application.Features.Product.Commands.UpdateProduct;
using B2B_Project.Application.Features.Product.Queries.GetAllProduct;
using B2B_Project.Application.Features.Product.Queries.GetByIdProduct;
using B2B_Project.Application.Features.Product.Queries.GetCompanyProductsByUsername;
using B2B_Project.Application.Features.Product.Queries.GetDefaultProductsByFilter;
using B2B_Project.Application.Features.Product.Queries.GetProductsByCategory;
using B2B_Project.Application.Features.Product.Queries.GetProductsByCompany;
using B2B_Project.Application.Features.Product.Queries.GetProductsByDynamicFilters;
using B2B_Project.Application.Features.Product.Queries.GetProductsCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(AuthenticationSchemes = "Bearer")]

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetAllProductQueryRequest request = new GetAllProductQueryRequest();
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsCount()
        {
            GetProductsCountQueryRequest request = new();
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdProductQueryRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Company")]
        [HttpGet]
        public async Task<IActionResult> GetCompanyProducts()
        {
            GetCompanyProductsByUsernameQueryRequest req = new();

            req.Username = User.FindFirstValue(ClaimTypes.Name);
            var data = await _mediator.Send(req);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByDefaultFilter([FromQuery] GetProductsByDefaultFilterQueryRequest req)
        {
            var data = await _mediator.Send(req);
            return Ok(data);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommandRequest req)
        {
            req.Username = User.FindFirstValue(ClaimTypes.Name);
            var res = _mediator.Send(req);
            return Ok(res.Result);
        }

        [HttpGet]//silinebilir olan
        public async Task<IActionResult> GetProductsByCompany([FromQuery] GetProductsByCompanyQueryRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory([FromQuery] GetProductsByCategoryQueryRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductCommandRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteById([FromQuery] RemoveProductCommandRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByDynamicFilters([FromQuery] GetProductsByDynamicFiltersCommandRequest req)
        {
            var res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}
