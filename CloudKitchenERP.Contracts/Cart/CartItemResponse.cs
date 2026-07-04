namespace CloudKitchenERP.Contracts.Cart;

public class CartItemResponse
{
    public int CartItemId { get; set; }

    public int MenuItemId { get; set; }

    public string MenuItemName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}