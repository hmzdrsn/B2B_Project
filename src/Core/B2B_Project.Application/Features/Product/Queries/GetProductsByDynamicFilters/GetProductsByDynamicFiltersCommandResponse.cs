
namespace B2B_Project.Application.Features.Product.Queries.GetProductsByDynamicFilters
{
    public class GetProductsByDynamicFiltersCommandResponse
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }

    }
}
