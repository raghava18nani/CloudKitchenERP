using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Contracts.Order;

public class UpdateOrderStatusRequest
{
    public int OrderId { get; set; }

    public OrderStatus Status { get; set; }
}