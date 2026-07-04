using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Application.Interfaces;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);

    Task<Order?> GetOrderByIdAsync(int orderId);

    Task<List<Order>> GetOrdersByUserIdAsync(int userId);

    Task SaveChangesAsync();
    Task<string> GenerateOrderNumberAsync();

    Task AddOrderItemAsync(OrderItem orderItem);
}