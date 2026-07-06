namespace CloudKitchenERP.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(
        string toEmail,
        string subject,
        string body);

    Task SendOtpEmailAsync(
        string toEmail,
        string otp);

    Task SendWelcomeEmailAsync(
    string toEmail,
    string customerName);

    Task SendOrderConfirmationEmailAsync(
    string toEmail,
    string customerName,
    string orderNumber,
    decimal amount);

    Task SendInvoiceEmailAsync(
    string toEmail,
    string customerName,
    byte[] invoicePdf,
    string invoiceNumber);
}