using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Interfaces;
using Product.Infrastructure.Repositories;

namespace Product.API.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
