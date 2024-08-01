using B2B_Project.Domain.Common;

namespace B2B_Project.Domain.Entities
{
    public class ProductAttribute : BaseEntity
    {
        public string? AttributeValue { get; set; }
        public Guid? AttributeTypeId { get; set; }
        public AttributeType? AttributeType { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
