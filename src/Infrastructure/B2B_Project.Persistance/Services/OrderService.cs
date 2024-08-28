using B2B_Project.Application;
using B2B_Project.Application.DTOs.Order;
using B2B_Project.Application.Features.Order.Commands.UpdateOrderStatus;
using B2B_Project.Application.Features.Order.Queries.GetOrderWithDetailsById;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly IOrderStatusReadRepository _orderStatusReadRepository;
        private readonly IImageReadRepository _imageReadRepository;
        private readonly UserManager<AppUser> _userManager;

        public OrderService(IOrderWriteRepository orderWriteRepository, UserManager<AppUser> userManager, IBasketReadRepository basketReadRepository, IOrderReadRepository orderReadRepository, ICompanyReadRepository companyReadRepository, IOrderStatusReadRepository orderStatusReadRepository, IImageReadRepository imageReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _userManager = userManager;
            _basketReadRepository = basketReadRepository;
            _orderReadRepository = orderReadRepository;
            _companyReadRepository = companyReadRepository;
            _orderStatusReadRepository = orderStatusReadRepository;
            _imageReadRepository = imageReadRepository;
        }

        public async Task<string> CreateOrderCode()
        {
            int orderCount =await _orderReadRepository.Table.CountAsync() + 1;

            return orderCount.ToString("D10");
        }

        public async Task<bool> CreateOrderAsync(CreateOrder model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return false;
            }

            //user'a ait basketi getir.
            var basket = await _basketReadRepository.GetAll()
                .Include(x => x.BasketItems)
                .ThenInclude(y => y.Product)
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id);
            if (basket == null)
            {
                return false;
            }
            //basketteki ürünleri al totalprice hesapla
            decimal TotalPrice = basket.BasketItems.Sum(x => x.Product.Price * x.Quantity) ?? 0;
            if (TotalPrice == 0)
            {
                return false;
            }

            var OrderStatus = await _orderStatusReadRepository.GetByIdAsync(model.OrderStatusId);

            //order olustur, order detail olusurken icerisindeki quantity ve UnitPrice degerlerini set et.
            Order order = new Order();
            order.AppUserId = user.Id;
            order.Address = model.Address;
            order.TotalPrice = TotalPrice;
            order.OrderDate = DateTime.Now;
            order.OrderStatus = OrderStatus;
            order.OrderCode =await CreateOrderCode();
            //order detail list olusturuldu
            List<OrderDetail> orderDetailList = new();
            foreach (var basketItem in basket.BasketItems)
            {
                OrderDetail orderDetail = new()
                {
                    Quantity = basketItem.Quantity ?? 0,
                    UnitPrice = basketItem.Product.Price ?? 0,
                    OrderDate = order.OrderDate,
                    OrderId = order.Id,
                    ProductId = basketItem.Product.Id
                };
                orderDetailList.Add(orderDetail);
            }
            order.OrderDetails = orderDetailList;

            //kayıt
            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();


            //sepet temizleme eklenebilir burada
            return true;
        }

        public async Task<List<GetOrdersByCompany>> GetOrdersByCompany(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }
            var company = await _companyReadRepository.Table.FirstOrDefaultAsync(x => x.PrimaryAppUserID == user.Id || x.SecondaryAppUserID == user.Id);

            if (company != null)//productın companyid'si company.id olanları getir.
            {
                var orders = await _orderReadRepository.GetAll()
                                .Include(x => x.OrderStatus)
                                .Include(x => x.AppUser)
                                .Include(x => x.OrderDetails)
                                .ThenInclude(p => p.Product)
                                .Where(x => x.OrderDetails.Any(x => x.Product.CompanyId == company.Id))
                                .Select(x => new GetOrdersByCompany()
                                {
                                    OrderId = x.Id.ToString(),
                                    Name = x.AppUser.Name,
                                    TotalPrice = x.TotalPrice,
                                    OrderDate = DateOnly.FromDateTime(x.OrderDate),
                                    OrderStatus = x.OrderStatus.Status
                                })
                                .ToListAsync();
                return orders;

            }
            return null;
        }

        public async Task<GetOrderWithDetailsByIdQueryResponse> GetOrderWithDetailsById(GetOrderWithDetailsByIdQueryRequest request)
        {
            var order =await _orderReadRepository.GetAll()
                .Where(x=>x.Id.ToString()==request.OrderId)
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderDetails)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync();
            if (order != null)
            {
                List<OrderDetailResponse> orderDetails = new();
                GetOrderWithDetailsByIdQueryResponse response = new();
                response.Address = order.Address;
                response.TotalPrice = order.TotalPrice;
                response.OrderCode = order.OrderCode;
                response.OrderDate = order.OrderDate;
                response.OrderStatus = order.OrderStatus.Status;

                foreach (var item in order.OrderDetails)
                {
                    orderDetails.Add(new OrderDetailResponse
                    {
                        ProductId = item.ProductId.ToString(),
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        ProductImageUrl = await _imageReadRepository.GetAll()
                         .Where(x => x.EntityId == item.ProductId.ToString())
                         .Select(x => x.ImageUrl)
                         .FirstOrDefaultAsync()
                    });
                }
                response.OrderDetails = orderDetails;

                return response;
            }
            return null;
        }

        public async Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusCommandRequest request)
        {
            var order = await _orderReadRepository.GetAll().FirstOrDefaultAsync(x => x.Id.ToString() == request.OrderId);
            if (order == null)
            {
                return false;
            }
            order.OrderStatusId = Guid.Parse(request.OrderStatusId);
            var res = _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveAsync();
            return res;
        }
    }
}
