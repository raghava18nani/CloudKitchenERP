using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Contracts.Payment;

public class PaymentResponse
{
    public int OrderId { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public PaymentMethod PaymentMethod { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }
}