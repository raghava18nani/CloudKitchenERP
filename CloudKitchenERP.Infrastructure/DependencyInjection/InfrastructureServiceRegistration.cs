using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;
using CloudKitchenERP.Infrastructure.Services;

namespace CloudKitchenERP.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAdminOrderService, AdminOrderService>();
        services.AddScoped<IDashboardService, DashboardService>();
        return services;
    }
}