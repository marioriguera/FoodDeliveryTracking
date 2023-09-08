using FoodDeliveryTracking.Services.Auth;
using FoodDeliveryTracking.Services.Auth.Implementations;
using FoodDeliveryTracking.Services.Encrypt;
using FoodDeliveryTracking.Services.Encrypt.Implementations;
using FoodDeliveryTracking.Services.Logger;
using FoodDeliveryTracking.Services.Logger.Implementations;

namespace FoodDeliveryTracking.Services.Register
{
    /// <summary>
    /// Register services layer dependency injections
    /// </summary>
    public static class ServicesDI
    {
        /// <summary>
        /// Configure and register the necessary dependencies in the application's dependency injection container.
        /// </summary>
        /// <param name="services">The collection of services to which the dependencies will be added.</param>
        /// <returns>The same collection of services with dependencies added.</returns>
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IEncryptService, EncryptService>();
            return services;
        }
    }
}
