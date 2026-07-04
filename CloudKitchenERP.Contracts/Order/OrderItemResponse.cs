namespace CloudKitchenERP.Contracts.Order;

public class OrderItemResponse
{
    public string MenuItemName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }
}