using CloudKitchenERP.Domain.Enums;

namespace CloudKitchenERP.Contracts.Order;

public class CheckoutRequest
{
    public PaymentMethod PaymentMethod { get; set; }
}