using B2B_Project.Application.DTOs.Order;

namespace B2B_Project.Application.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(CreateOrder model);
        Task<List<GetOrdersByCompany>> GetOrdersByCompany(string username);
    }
}
