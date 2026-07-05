using CloudKitchenERP.Application.Interfaces;

namespace CloudKitchenERP.Infrastructure.Services;

public class MockSmsService : ISmsService
{
    public Task SendOtpAsync(string mobileNumber, string otp)
    {
        Console.WriteLine(
            $"OTP for {mobileNumber} : {otp}");

        return Task.CompletedTask;
    }
}