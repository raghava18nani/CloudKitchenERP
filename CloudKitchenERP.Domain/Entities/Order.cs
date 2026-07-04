using CloudKitchenERP.Domain.Common;
using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal SubTotal { get; set; }

    public decimal DeliveryCharge { get; set; }

    public decimal Tax { get; set; }

    public decimal GrandTotal { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    public string DeliveryAddress { get; set; } = string.Empty;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}