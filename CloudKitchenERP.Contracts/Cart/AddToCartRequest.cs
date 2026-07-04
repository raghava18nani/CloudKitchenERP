namespace CloudKitchenERP.Contracts.Cart;

public class AddToCartRequest
{
    public int MenuItemId { get; set; }

    public int Quantity { get; set; }
}