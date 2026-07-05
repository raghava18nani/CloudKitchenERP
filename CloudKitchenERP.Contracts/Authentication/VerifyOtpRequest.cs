using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Contracts.Authentication;

public class VerifyOtpRequest
{
    public string MobileNumber { get; set; } = string.Empty;

    public string Otp { get; set; } = string.Empty;

    public OtpPurpose Purpose { get; set; }
}