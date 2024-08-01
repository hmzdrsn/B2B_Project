using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Entities;
using MediatR;

namespace B2B_Project.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, HandlerResponse<CreateProductCommandResponse>>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<HandlerResponse<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId != null ? Guid.Parse(request.CategoryId) : null
            }))
            {
                await _productWriteRepository.SaveAsync();
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
