using B2B_Project.Application.DTOs.Order;
using B2B_Project.Application.Repositories;
using B2B_Project.Application.Services;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using B2B_Project.Persistance.Repositories;
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
        private readonly UserManager<AppUser> _userManager;

        public OrderService(IOrderWriteRepository orderWriteRepository, UserManager<AppUser> userManager, IBasketReadRepository basketReadRepository, IOrderReadRepository orderReadRepository, ICompanyReadRepository companyReadRepository, IOrderStatusReadRepository orderStatusReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _userManager = userManager;
            _basketReadRepository = basketReadRepository;
            _orderReadRepository = orderReadRepository;
            _companyReadRepository = companyReadRepository;
            _orderStatusReadRepository = orderStatusReadRepository;
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
                                .Include(x=>x.OrderStatus)
                                .Include(x => x.AppUser)
                                .Include(x => x.OrderDetails)
                                .ThenInclude(p => p.Product)
                                .Where(x => x.OrderDetails.Any(x => x.Product.CompanyId == company.Id))
                                .Select(x => new GetOrdersByCompany()
                                {
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
    }
}
