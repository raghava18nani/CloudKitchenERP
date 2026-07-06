namespace CloudKitchenERP.Infrastructure.Configuration;

public class EmailSettings
{
    public string FromEmail { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string SmtpServer { get; set; } = string.Empty;

    public int Port { get; set; }

    public string DisplayName { get; set; } = string.Empty;
}