using CloudKitchenERP.Contracts.Authentication;

namespace CloudKitchenERP.Application.Interfaces;

public interface IAuthenticationService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<CurrentUserResponse?> GetCurrentUserAsync(int userId);

    Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
}