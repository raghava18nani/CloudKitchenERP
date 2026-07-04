using CloudKitchenERP.Contracts.Order;

namespace CloudKitchenERP.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CheckoutAsync(int userId, CheckoutRequest request);

    Task<List<OrderResponse>> GetMyOrdersAsync(int userId);

    Task<OrderResponse?> GetByIdAsync(int userId, int orderId);

    Task<bool> UpdateStatusAsync(UpdateOrderStatusRequest request);
}