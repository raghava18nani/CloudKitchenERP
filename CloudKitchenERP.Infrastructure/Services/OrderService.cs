using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Order;
using CloudKitchenERP.Domain.Entities;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderResponse> CheckoutAsync(int userId, CheckoutRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Step 1: Get Cart
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                throw new Exception("Cart not found.");

            var cartItems = await _cartRepository.GetCartItemsAsync(cart.Id);

            // Step 2: Validate Cart
            if (cartItems.Count == 0)
                throw new Exception("Cart is empty.");

            // Step 3: Generate Order Number
            var orderNumber = await _orderRepository.GenerateOrderNumberAsync();
            var subTotal = cartItems.Sum(x => x.TotalPrice);

            var deliveryCharge = subTotal >= 500 ? 0 : 40;

            var tax = Math.Round(subTotal * 0.05m, 2);

            var grandTotal = subTotal + deliveryCharge + tax;

            // Step 4: Create Order
            var order = new Order
            {
                OrderNumber = orderNumber,
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,

                PaymentMethod = request.PaymentMethod,
                PaymentStatus = PaymentStatus.Pending,

                SubTotal = subTotal,
                DeliveryCharge = deliveryCharge,
                Tax = tax,
                GrandTotal = grandTotal,

                DeliveryAddress = "" // We'll fetch this from Customer later
            };

           
            // Step 5: Create Order Items
            await _orderRepository.AddOrderAsync(order);
            await _unitOfWork.SaveChangesAsync();
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                };

                await _orderRepository.AddOrderItemAsync(orderItem);
            }
            // Step 6: Clear Cart
            await _cartRepository.ClearCartAsync(cart.Id);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return new OrderResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                OrderDate = order.OrderDate,
                GrandTotal = order.GrandTotal,
                Items = cartItems.Select(x => new OrderItemResponse
                {
                    MenuItemName = x.MenuItem.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    TotalPrice = x.TotalPrice
                }).ToList()
            };
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    public async Task<List<OrderResponse>> GetMyOrdersAsync(int userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

        return orders.Select(order => new OrderResponse
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
            OrderDate = order.OrderDate,
            GrandTotal = order.GrandTotal,

            Items = order.OrderItems.Select(item => new OrderItemResponse
            {
                MenuItemName = item.MenuItem.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice
            }).ToList()

        }).ToList();
    }

    public async Task<OrderResponse?> GetByIdAsync(int userId, int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        if (order == null)
            return null;

        if (order.UserId != userId)
            return null;

        return new OrderResponse
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
            OrderDate = order.OrderDate,
            GrandTotal = order.GrandTotal,

            Items = order.OrderItems.Select(item => new OrderItemResponse
            {
                MenuItemName = item.MenuItem.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice
            }).ToList()
        };
    }

    public Task<bool> UpdateStatusAsync(UpdateOrderStatusRequest request)
    {
        throw new NotImplementedException();
    }
}