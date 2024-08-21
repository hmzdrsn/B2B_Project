using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, HandlerResponse<CreateProductCommandResponse>>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService, ICompanyReadRepository companyReadRepository)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            bool res = await _productService.CreateProductAsync(new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ProductCode = request.ProductCode,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                Username = request.Username,
                ProductImages = request.ProductImages
            });
            if (res)
            {
                return new()
                {
                    Message = "Product Successfully Added.",
                    Status = "Added",
                    Data = new()
                };
            }
            return new()
            {
                Message = "An error occurred while adding the product.",
                Status = "Error",
                Data = new()
            };
        }
    }
}
