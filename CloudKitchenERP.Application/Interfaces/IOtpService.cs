using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Application.Interfaces;

public interface IOtpService
{
    Task SendOtpAsync(
        string mobileNumber,
        string? email,
        OtpPurpose purpose);
    Task<bool> VerifyOtpAsync(
        string mobileNumber,
        string otp,
        OtpPurpose purpose);

    Task ResendOtpAsync(
        string mobileNumber,
        string? email,
        OtpPurpose purpose);
}