using Microsoft.Extensions.DependencyInjection;
using Quotation.Application.Interfaces;
using Quotation.Application.Services;

namespace Quotation.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.ServicesInit();

            return services;
        }

        private static void ServicesInit(this IServiceCollection services)
        {
            services.AddScoped<IQuotationService, QuotationService>();
        }
    }
}
