using CloudKitchenERP.Domain.Common;

namespace CloudKitchenERP.Domain.Entities;

public class User : BaseEntity
{
    public int RoleId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public string MobileNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public bool IsMobileVerified { get; set; }

    public bool IsEmailVerified { get; set; }

    public DateTime? LastLogin { get; set; }

    public Role Role { get; set; } = null!;
}
