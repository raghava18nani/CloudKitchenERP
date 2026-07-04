using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Authentication;
using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
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

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            return new LoginResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            return new LoginResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        return new LoginResponse
        {
            Success = true,
            Message = "Login successful.",
            Token = _jwtTokenGenerator.GenerateToken(user)
        };
    }

    public async Task<CurrentUserResponse?> GetCurrentUserAsync(int userId)
    {
        var user = await _userRepository.GetByIdWithRoleAsync(userId);

        if (user == null)
            return null;

        return new CurrentUserResponse
        {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Email = user.Email ?? "",
            MobileNumber = user.MobileNumber,
            Role = user.Role.Name
        };
    }
}