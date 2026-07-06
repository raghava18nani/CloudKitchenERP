using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Contracts.Authentication;

public class SendOtpRequest
{
    public string MobileNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public OtpPurpose Purpose { get; set; }
}