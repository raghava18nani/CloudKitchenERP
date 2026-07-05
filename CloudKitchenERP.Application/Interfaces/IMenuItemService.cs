using CloudKitchenERP.Contracts.MenuItem;
using System.IO;

namespace CloudKitchenERP.Application.Interfaces;

public interface IMenuItemService
{
    Task<List<MenuItemResponse>> GetAllAsync();

    Task<MenuItemResponse?> GetByIdAsync(int id);

    Task<MenuItemResponse> CreateAsync(CreateMenuItemRequest request);

    Task<bool> UpdateAsync(UpdateMenuItemRequest request);

    Task<bool> DeleteAsync(int id);

    Task<bool> UpdateImageUrlAsync(int menuItemId, string imageUrl);
}