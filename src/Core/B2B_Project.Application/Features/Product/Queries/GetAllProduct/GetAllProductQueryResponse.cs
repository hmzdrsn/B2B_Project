using B2B_Project.Application.DTOs.Product;

namespace B2B_Project.Application.Features.Product.Queries.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public List<DTOs.Product.GetAllProduct>? Products { get; set; }
    }
}
