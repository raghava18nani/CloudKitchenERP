using CloudKitchenERP.Domain.Common;

namespace CloudKitchenERP.Domain.Entities;

public class Customer : BaseEntity
{
    public int UserId { get; set; }

    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Pincode { get; set; } = string.Empty;

    public string? Landmark { get; set; }

    public User User { get; set; } = null!;
} 