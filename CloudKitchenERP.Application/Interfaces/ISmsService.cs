namespace CloudKitchenERP.Application.Interfaces;

public interface ISmsService
{
    Task SendOtpAsync(string mobileNumber, string otp);
}