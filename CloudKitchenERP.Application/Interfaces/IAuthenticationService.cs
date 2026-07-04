using CloudKitchenERP.Contracts.Authentication;

namespace CloudKitchenERP.Application.Interfaces;

public interface IAuthenticationService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
}