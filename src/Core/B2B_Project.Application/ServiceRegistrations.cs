using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace B2B_Project.Application
{
    public static class ServiceRegistrations
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
