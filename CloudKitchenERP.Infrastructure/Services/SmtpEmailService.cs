using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CloudKitchenERP.Infrastructure.Services;

public class SmtpEmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public SmtpEmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string body)
    {
        using var client = new SmtpClient(
            _settings.SmtpServer,
            _settings.Port);

        client.EnableSsl = true;

        client.Credentials = new NetworkCredential(
            _settings.FromEmail,
            _settings.Password);

        var mail = new MailMessage
        {
            From = new MailAddress(
                _settings.FromEmail,
                _settings.DisplayName),

            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mail.To.Add(toEmail);

        await client.SendMailAsync(mail);
    }

    public async Task SendOtpEmailAsync(
     string toEmail,
     string otp)
    {
        var body = $@"
    <div style='font-family: Arial, Helvetica, sans-serif; max-width:600px; margin:auto; border:1px solid #ddd; border-radius:10px; overflow:hidden;'>

        <div style='background-color:#8B0000; color:white; padding:20px; text-align:center;'>
            <h2 style='margin:0;'>🍲 Telugu Inti Ruchulu</h2>
            <p style='margin:5px 0 0 0;'>Authentic Telugu Homemade Food</p>
        </div>

        <div style='padding:25px;'>

            <p>Hello,</p>

            <p>Thank you for choosing <b>Telugu Inti Ruchulu</b>.</p>

            <p>Your One-Time Password (OTP) for verification is:</p>

            <div style='text-align:center; margin:25px 0;'>
                <span style='font-size:32px;
                             font-weight:bold;
                             letter-spacing:6px;
                             color:#d35400;
                             background:#fff3cd;
                             padding:12px 25px;
                             border-radius:8px;
                             display:inline-block;'>
                    {otp}
                </span>
            </div>

            <p>
                This OTP is valid for <b>5 minutes</b>.
            </p>

            <p>
                Please do not share this OTP with anyone for security reasons.
            </p>

            <hr/>

            <p>
                If you didn't request this OTP, you can safely ignore this email.
            </p>

            <br/>

            <p>
                Thank you for choosing us.
            </p>

            <p>
                Regards,<br/>
                <b>Team Telugu Inti Ruchulu</b>
            </p>

        </div>

        <div style='background:#f5f5f5; padding:15px; text-align:center; font-size:12px; color:#666;'>
            © 2026 Telugu Inti Ruchulu. All Rights Reserved.
        </div>

    </div>";

        await SendEmailAsync(
            toEmail,
            "🍲 Telugu Inti Ruchulu - OTP Verification",
            body);
    }

    public async Task SendWelcomeEmailAsync(
    string toEmail,
    string customerName)
    {
        var body = $@"
    <h2>🍲 Welcome to Telugu Inti Ruchulu</h2>

    <p>Dear {customerName},</p>

    <p>Your account has been created successfully.</p>

    <p>
        Thank you for joining Telugu Inti Ruchulu.
        We look forward to serving you authentic Telugu homemade food.
    </p>

    <p>Happy Ordering!</p>

    <br/>

    <b>Team Telugu Inti Ruchulu</b>";

        await SendEmailAsync(
            toEmail,
            "Welcome to Telugu Inti Ruchulu",
            body);
    }

    public async Task SendOrderConfirmationEmailAsync(
    string toEmail,
    string customerName,
    string orderNumber,
    decimal amount)
    {
        var body = $@"
    <h2>🍲 Order Confirmed</h2>

    <p>Hello {customerName},</p>

    <p>Your order has been confirmed.</p>

    <p><b>Order Number:</b> {orderNumber}</p>

    <p><b>Total Amount:</b> ₹{amount}</p>

    <p>We'll start preparing your order shortly.</p>

    <br/>

    <b>Thank you for choosing Telugu Inti Ruchulu.</b>";

        await SendEmailAsync(
            toEmail,
            "Order Confirmed",
            body);
    }

    public async Task SendInvoiceEmailAsync(
    string toEmail,
    string customerName,
    byte[] invoicePdf,
    string invoiceNumber)
    {
        using var client = new SmtpClient(
            _settings.SmtpServer,
            _settings.Port);

        client.EnableSsl = true;

        client.Credentials = new NetworkCredential(
            _settings.FromEmail,
            _settings.Password);

        var mail = new MailMessage
        {
            From = new MailAddress(
                _settings.FromEmail,
                _settings.DisplayName),

            Subject = $"Invoice - Telugu Inti Ruchulu ({invoiceNumber})",

            IsBodyHtml = true,

            Body = $@"
        <h2>🍲 Telugu Inti Ruchulu</h2>

        <p>Hello <b>{customerName}</b>,</p>

        <p>Thank you for ordering from Telugu Inti Ruchulu.</p>

        <p>Your invoice is attached with this email.</p>

        <p>We hope you enjoy our authentic homemade Telugu food.</p>

        <br/>

        <p>Regards,</p>

        <b>Team Telugu Inti Ruchulu</b>"
        };

        mail.To.Add(toEmail);

        var stream = new MemoryStream(invoicePdf);

        mail.Attachments.Add(
            new Attachment(
                stream,
                $"Invoice_{invoiceNumber}.pdf",
                "application/pdf"));

        await client.SendMailAsync(mail);
    }
}