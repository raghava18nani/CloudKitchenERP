using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Infrastructure.Invoice;
using QuestPDF.Fluent;

namespace CloudKitchenERP.Infrastructure.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IOrderRepository _orderRepository;

    public InvoiceService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<byte[]> GenerateInvoiceAsync(int userId, int orderId)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(orderId);

        if (order == null)
            throw new Exception("Order not found.");

        if (order.UserId != userId)
            throw new Exception("Unauthorized.");

        var document = new InvoiceDocument(order);

        return document.GeneratePdf();
    }
}