using IbanChecker.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IbanChecker
{
    public static class IbanExtension
    {

        public static IServiceCollection AddIbanChecker(this IServiceCollection services)
        {
            services.AddScoped<IBankCheckerService, BankCheckerService>();

            return services;
        }
    }
}
