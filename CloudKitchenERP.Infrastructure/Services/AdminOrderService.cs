using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Order;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Infrastructure.Services;

public class AdminOrderService : IAdminOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    public AdminOrderService(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<List<OrderResponse>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();

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

    public async Task<List<OrderResponse>> GetOrdersByStatusAsync(OrderStatus status)
    {
        var orders = await _orderRepository.GetOrdersByStatusAsync(status);

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

    public async Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusRequest request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);

        if (order == null)
            return false;
        order.Status = request.Status;

        await _orderRepository.UpdateOrderAsync(order);
        await _unitOfWork.SaveChangesAsync();

        // Send status email
        var customer = await _userRepository.GetByIdAsync(order.UserId);

        if (customer != null)
        {
            await _emailService.SendOrderStatusEmailAsync(
                customer.Email,
                customer.FirstName,
                order.OrderNumber,
                order.Status.ToString());
        }

        return true;
    }
}