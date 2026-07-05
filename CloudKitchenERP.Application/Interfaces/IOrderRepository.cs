using CloudKitchenERP.Domain.Entities;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Application.Interfaces;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);

    Task<Order?> GetOrderByIdAsync(int orderId);

    Task<List<Order>> GetOrdersByUserIdAsync(int userId);

    Task SaveChangesAsync();
    Task<string> GenerateOrderNumberAsync();

    Task AddOrderItemAsync(OrderItem orderItem);

    Task<List<Order>> GetAllOrdersAsync();

    Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status);

    Task UpdateOrderAsync(Order order);

    Task<Order?> GetOrderWithItemsAsync(int orderId);
}