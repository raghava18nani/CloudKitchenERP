using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Payment;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IOrderRepository _orderRepository;

    public PaymentService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PaymentResponse?> PayAsync(int userId, PayOrderRequest request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);

        if (order == null)
            return null;

        if (order.UserId != userId)
            return null;

        // Don't allow payment twice
        if (order.PaymentStatus == PaymentStatus.Paid)
            throw new Exception("Order is already paid.");

        order.PaymentMethod = request.PaymentMethod;

        switch (request.PaymentMethod)
        {
            case PaymentMethod.CashOnDelivery:
                order.PaymentStatus = PaymentStatus.Pending;
                break;

            case PaymentMethod.UPI:
                // Simulate successful UPI payment
                order.PaymentStatus = PaymentStatus.Paid;
                break;

            default:
                throw new Exception("Unsupported payment method.");
        }

        await _orderRepository.UpdateOrderAsync(order);
        await _orderRepository.SaveChangesAsync();

        return new PaymentResponse
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
            Amount = order.GrandTotal,
            PaymentDate = DateTime.UtcNow
        };
    }
    public async Task<List<PaymentResponse>> GetPaymentHistoryAsync(int userId)
    {
        var orders = await _orderRepository.GetOrdersWithPaymentsAsync(userId);

        return orders.Select(order => new PaymentResponse
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
            Amount = order.GrandTotal,
            PaymentDate = order.OrderDate
        }).ToList();
    }
}