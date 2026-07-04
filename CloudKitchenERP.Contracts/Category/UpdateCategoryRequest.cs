namespace CloudKitchenERP.Contracts.Category;

public class UpdateCategoryRequest
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}