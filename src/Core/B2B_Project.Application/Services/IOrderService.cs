using B2B_Project.Application.DTOs.Order;
using B2B_Project.Application.Features.Order.Commands.UpdateOrderStatus;
using B2B_Project.Application.Features.Order.Queries.GetOrderWithDetailsById;

namespace B2B_Project.Application.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(CreateOrder model);
        Task<List<GetOrdersByCompany>> GetOrdersByCompany(string username);
        Task<GetOrderWithDetailsByIdQueryResponse> GetOrderWithDetailsById(GetOrderWithDetailsByIdQueryRequest request);

        Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusCommandRequest request);
    }
}
