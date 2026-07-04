using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CloudKitchenERP.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}