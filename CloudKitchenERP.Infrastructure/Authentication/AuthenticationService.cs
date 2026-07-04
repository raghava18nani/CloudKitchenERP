using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Authentication;
using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        // Check email
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            return new RegisterResponse
            {
                Success = false,
                Message = "Email already exists."
            };
        }

        // Check mobile
        if (await _userRepository.MobileExistsAsync(request.MobileNumber))
        {
            return new RegisterResponse
            {
                Success = false,
                Message = "Mobile number already exists."
            };
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            MobileNumber = request.MobileNumber,
            RoleId = 2, // Customer (temporary)
            IsActive = true,
            IsEmailVerified = false,
            IsMobileVerified = false
        };

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new RegisterResponse
        {
            Success = true,
            Message = "Registration successful."
        };
    }
}