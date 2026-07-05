namespace CloudKitchenERP.Infrastructure.Configuration;

public class SmsSettings
{
    public string Provider { get; set; } = "Mock";

    public string AuthKey { get; set; } = string.Empty;

    public string SenderId { get; set; } = string.Empty;

    public string TemplateId { get; set; } = string.Empty;
}