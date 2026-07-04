using CloudKitchenERP.Contracts.Order;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Application.Interfaces;

public interface IAdminOrderService
{
    Task<List<OrderResponse>> GetAllOrdersAsync();

    Task<List<OrderResponse>> GetOrdersByStatusAsync(OrderStatus status);

    Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusRequest request);
}