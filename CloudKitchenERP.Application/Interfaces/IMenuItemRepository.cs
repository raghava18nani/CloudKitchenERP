using CloudKitchenERP.Contracts.MenuItem;
using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Application.Interfaces;

public interface IMenuItemRepository
{
    Task<List<MenuItem>> GetAllAsync();

    Task<MenuItem?> GetByIdAsync(int id);

    Task AddAsync(MenuItem menuItem);

    Task UpdateAsync(MenuItem menuItem);

    Task DeleteAsync(MenuItem menuItem);

    Task SaveChangesAsync();

    Task<(List<MenuItem> Items, int TotalRecords)> SearchAsync(MenuItemSearchRequest request);
}