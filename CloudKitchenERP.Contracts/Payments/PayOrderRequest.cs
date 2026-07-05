using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Contracts.Payment;

public class PayOrderRequest
{
    public int OrderId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
}