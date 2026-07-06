using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Infrastructure.Authentication;
using CloudKitchenERP.Infrastructure.Configuration;
using CloudKitchenERP.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace CloudKitchenERP.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
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
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ISmsService, MockSmsService>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IEmailService, SmtpEmailService>();

        services.Configure<EmailSettings>(
            configuration.GetSection("EmailSettings"));

        return services;
    }
}