using B2B_Project.Application.Repositories;
using B2B_Project.Persistance.Context;
using B2B_Project.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Persistance
{
    public static class ServiceRegistrations
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<B2B_ProjectDbContext>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

            services.AddScoped<IAttributeTypeReadRepository, AttributeTypeReadRepository>();
            services.AddScoped<IAttributeTypeWriteRepository, AttributeTypeWriteRepository>();

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped<IOrderDetailReadRepository, OrderDetailReadRepository>();
            services.AddScoped<IOrderDetailWriteRepository, OrderDetailWriteRepository>();

            services.AddScoped<IProductAttributeReadRepository, ProductAttributeReadRepository>();
            services.AddScoped<IProductAttributeWriteRepository, ProductAttributeWriteRepository>();
        }
    }
}
