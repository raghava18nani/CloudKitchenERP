using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Domain.Entities;
using CloudKitchenERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Persistence.Repositories;

public class OtpRepository : IOtpRepository
{
    private readonly ApplicationDbContext _context;

    public OtpRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OtpVerification?> GetLatestOtpAsync(string mobileNumber, OtpPurpose purpose)
    {
        return await _context.OtpVerifications
            .Where(x =>
                x.MobileNumber == mobileNumber &&
                x.Purpose == purpose)
            .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(OtpVerification otp)
    {
        await _context.OtpVerifications.AddAsync(otp);
    }

    public Task UpdateAsync(OtpVerification otp)
    {
        _context.OtpVerifications.Update(otp);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsOtpVerifiedAsync(
    string mobileNumber,
    OtpPurpose purpose)
    {
        return await _context.OtpVerifications.AnyAsync(x =>
            x.MobileNumber == mobileNumber &&
            x.Purpose == purpose &&
            x.IsVerified);
    }
}