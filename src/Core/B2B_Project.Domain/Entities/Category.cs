using B2B_Project.Domain.Common;

namespace B2B_Project.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid ParentCategoryId{ get; set; }
        ICollection<Product>? Products { get; set;}
    }
}
