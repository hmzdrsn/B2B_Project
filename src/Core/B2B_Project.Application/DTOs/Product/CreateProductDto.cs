using Microsoft.AspNetCore.Http;

namespace B2B_Project.Application.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string ProductCode { get; set; } //
        public int? Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Username { get; set; }
        public List<IFormFile>? ProductImages { get; set; }
    }
}
