using CloudKitchenERP.Domain.Common;

namespace CloudKitchenERP.Domain.Entities;

public class CartItem : BaseEntity
{
    public int CartId { get; set; }

    public Cart Cart { get; set; } = null!;

    public int MenuItemId { get; set; }

    public MenuItem MenuItem { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }
}