namespace CloudKitchenERP.Application.Interfaces;

public interface IInvoiceService
{
    Task<byte[]> GenerateInvoiceAsync(int userId, int orderId);
}