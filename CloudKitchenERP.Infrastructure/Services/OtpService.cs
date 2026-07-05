using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Domain.Entities;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Infrastructure.Services;

public class OtpService : IOtpService
{
    private readonly IOtpRepository _otpRepository;
    private readonly ISmsService _smsService;

    public OtpService(
        IOtpRepository otpRepository,
        ISmsService smsService)
    {
        _otpRepository = otpRepository;
        _smsService = smsService;
    }

    public async Task SendOtpAsync(string mobileNumber, OtpPurpose purpose)
    {
        var random = new Random();

        var otp = random.Next(100000, 999999).ToString();

        var entity = new OtpVerification
        {
            MobileNumber = mobileNumber,
            OtpCode = otp,
            Purpose = purpose,
            CreatedDate = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            IsVerified = false,
            AttemptCount = 0,
            IsActive = true
        };

        await _otpRepository.AddAsync(entity);
        await _otpRepository.SaveChangesAsync();

        await _smsService.SendOtpAsync(mobileNumber, otp);
    }


    public async Task<bool> VerifyOtpAsync(
    string mobileNumber,
    string otp,
    OtpPurpose purpose)
    {
        var entity = await _otpRepository
            .GetLatestOtpAsync(mobileNumber, purpose);

        if (entity == null)
            return false;

        if (entity.IsVerified)
            return false;

        if (entity.ExpiresAt < DateTime.UtcNow)
            return false;

        entity.AttemptCount++;

        if (entity.OtpCode != otp)
        {
            await _otpRepository.UpdateAsync(entity);
            await _otpRepository.SaveChangesAsync();
            return false;
        }

        entity.IsVerified = true;
        entity.VerifiedAt = DateTime.UtcNow;

        await _otpRepository.UpdateAsync(entity);
        await _otpRepository.SaveChangesAsync();

        return true;
    }

    public async Task ResendOtpAsync(string mobileNumber, OtpPurpose purpose)
    {
        await SendOtpAsync(mobileNumber, purpose);
    }
    // Methods will be added next...
}