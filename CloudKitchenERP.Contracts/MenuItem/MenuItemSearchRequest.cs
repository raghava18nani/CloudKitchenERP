namespace CloudKitchenERP.Contracts.MenuItem;

public class MenuItemSearchRequest
{
    public string? Search { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsVeg { get; set; }

    public bool? IsAvailable { get; set; }

    public string? SortBy { get; set; } = "name";

    public string? SortOrder { get; set; } = "asc";

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}