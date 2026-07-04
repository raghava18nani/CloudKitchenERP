namespace CloudKitchenERP.Contracts.Authentication;

public class CurrentUserResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}