using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Features.Product.Commands.CreateProduct;
using B2B_Project.Application.Services;
using FluentValidation;
using MediatR;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, HandlerResponse<CreateProductCommandResponse>>
{
    private readonly IValidator<CreateProductCommandRequest> _validator;
    private readonly IProductService _productService;

    public CreateProductCommandHandler(IValidator<CreateProductCommandRequest> validator, IProductService productService)
    {
        _validator = validator;
        _productService = productService;
    }

    public async Task<HandlerResponse<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));

            return new()
            {
                Message = $"Validasyon hataları: {errorMessages}",
                Status = "ValidationError",
                Data = null
            };
        }

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
            return new HandlerResponse<CreateProductCommandResponse>
            {
                Message = "Ürün başarıyla eklendi.",
                Status = "Added",
                Data = new CreateProductCommandResponse()
            };
        }

        return new HandlerResponse<CreateProductCommandResponse>
        {
            Message = "Ürün eklenirken bir hata oluştu.",
            Status = "Error",
            Data = null
        };
    }
}
