namespace B2B_Project.API.Models
{
    public class AddProductToBasketDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
