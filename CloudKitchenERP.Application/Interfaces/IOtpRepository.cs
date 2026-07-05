using CloudKitchenERP.Domain.Entities;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Application.Interfaces;

public interface IOtpRepository
{
    Task<OtpVerification?> GetLatestOtpAsync(string mobileNumber, OtpPurpose purpose);

    Task AddAsync(OtpVerification otp);

    Task UpdateAsync(OtpVerification otp);

    Task SaveChangesAsync();

    Task<bool> IsOtpVerifiedAsync(
    string mobileNumber,
    OtpPurpose purpose);
}