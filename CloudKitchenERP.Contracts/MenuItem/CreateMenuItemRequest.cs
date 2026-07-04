namespace CloudKitchenERP.Contracts.MenuItem;

public class CreateMenuItemRequest
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsVeg { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int PreparationTime { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsBestSeller { get; set; }

    public bool IsTodaySpecial { get; set; }
}