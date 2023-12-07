using Er.Core.Helpers;
using Er.Core.Interfaces;
using Er.Core.Services;
using Er.Core.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Er.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<SalutationHelper>();
            services.AddTransient<CountryHelper>();

            services.AddTransient<RebateValidator>();

            services.AddScoped<IRebateOfferService, RebateOfferService>();
            services.AddScoped<IRebateService, RebateService>();

            return services;
        }
    }
}
