using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Authentication;
using CloudKitchenERP.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IOtpService _otpService;
    private readonly IEmailService _emailService;

    public AuthController(
       IAuthenticationService authenticationService,
       IOtpService otpService, IEmailService emailService)
    {
        _authenticationService = authenticationService;
        _otpService = otpService;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authenticationService.RegisterAsync(request);

        if (!result.Success)
        {
            return Conflict(result); // HTTP 409
        }

        return Ok(result); // HTTP 200
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authenticationService.LoginAsync(request);

        if (!result.Success)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }


    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized();
        }

        var user = await _authenticationService.GetCurrentUserAsync(int.Parse(userIdClaim));

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp(SendOtpRequest request)
    {
        await _otpService.SendOtpAsync(
     request.MobileNumber,
     request.Email,
     request.Purpose);

        return Ok(new
        {
            Message = "OTP sent successfully."
        });
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpRequest request)
    {
        var success = await _otpService.VerifyOtpAsync(
            request.MobileNumber,
            request.Otp,
            request.Purpose);

        if (!success)
            return BadRequest(new
            {
                Message = "Invalid or expired OTP."
            });

        return Ok(new
        {
            Message = "OTP verified successfully."
        });
    }

    [HttpPost("resend-otp")]
    public async Task<IActionResult> ResendOtp(SendOtpRequest request)
    {
        await _otpService.ResendOtpAsync(
       request.MobileNumber,
       request.Email,
       request.Purpose);

        return Ok(new
        {
            Message = "OTP resent successfully."
        });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
    ResetPasswordRequest request)
    {
        var success =
            await _authenticationService.ResetPasswordAsync(request);

        if (!success)
            return BadRequest("OTP verification required.");

        return Ok("Password changed successfully.");
    }
}
