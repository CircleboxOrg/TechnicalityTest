using Microsoft.Extensions.DependencyInjection;

namespace TechnicalityTestAPI.Extensions
{
    public static class IServiceCollectionExtension
    {

        public static IServiceCollection RegisterAppRepositories(this IServiceCollection services)
        {

            services.AddScoped<ICCChargeRepository, CCChargeRepository>();
            return services;
        }

        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {

            services.AddScoped<ICCChargeService, CCChargeService>();
            return services;
        }

    }
}