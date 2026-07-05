using CloudKitchenERP.Domain.Common;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Domain.Entities;

public class OtpVerification : BaseEntity
{
    public string MobileNumber { get; set; } = string.Empty;

    public string OtpCode { get; set; } = string.Empty;

    public OtpPurpose Purpose { get; set; }
    public DateTime ExpiresAt { get; set; }

    public bool IsVerified { get; set; }

    public int AttemptCount { get; set; }

    public DateTime? VerifiedAt { get; set; }

}