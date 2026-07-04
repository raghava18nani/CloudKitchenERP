namespace CloudKitchenERP.Contracts.Customer;

public class CreateCustomerRequest
{
    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Pincode { get; set; } = string.Empty;

    public string? Landmark { get; set; }
}