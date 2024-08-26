using B2B_Project.Application.DTOs.Product;
using B2B_Project.Application.Features.Product.Commands.UpdateProduct;
using B2B_Project.Application.Features.Product.Queries.GetByIdProduct;
using B2B_Project.Domain.Entities;

namespace B2B_Project.Application.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId);
        Task<List<Product>> GetCompanyProductsByUsername(string userName);
        Task<bool> CreateProductAsync(CreateProductDto model);
        Task<bool> UpdateProductAsync(UpdateProductCommandRequest request);
        Task<bool> DeleteById(Guid productId);
        Task<GetByIdProductQueryResponse> GetProductByIdWithImageAndCategory(GetByIdProductQueryRequest request);
    }
}
