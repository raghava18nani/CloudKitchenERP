using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Contracts.Order;

public class OrderResponse
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public OrderStatus Status { get; set; }

    public decimal GrandTotal { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public DateTime OrderDate { get; set; }

    public List<OrderItemResponse> Items { get; set; } = new();
}