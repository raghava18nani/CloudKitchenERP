using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Domain.Entities;
using CloudKitchenERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using CloudKitchenERP.Contracts.MenuItem;

namespace CloudKitchenERP.Persistence.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly ApplicationDbContext _context;

    public MenuItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItems
            .Include(x => x.Category)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _context.MenuItems
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(MenuItem menuItem)
    {
        await _context.MenuItems.AddAsync(menuItem);
    }

    public Task UpdateAsync(MenuItem menuItem)
    {
        _context.MenuItems.Update(menuItem);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(MenuItem menuItem)
    {
        _context.MenuItems.Remove(menuItem);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<(List<MenuItem> Items, int TotalRecords)> SearchAsync(MenuItemSearchRequest request)
    {
        IQueryable<MenuItem> query = _context.MenuItems
            .Include(x => x.Category);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.Name.ToLower().Contains(search) ||
                x.Description.ToLower().Contains(search));
        }

        // Category Filter
        if (request.CategoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == request.CategoryId.Value);
        }

        // Veg Filter
        if (request.IsVeg.HasValue)
        {
            query = query.Where(x => x.IsVeg == request.IsVeg.Value);
        }

        // Availability Filter
        if (request.IsAvailable.HasValue)
        {
            query = query.Where(x => x.IsAvailable == request.IsAvailable.Value);
        }

        // Sorting
        switch (request.SortBy?.ToLower())
        {
            case "price":
                query = request.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Price)
                    : query.OrderBy(x => x.Price);
                break;

            case "name":
            default:
                query = request.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name);
                break;
        }

        var totalRecords = await query.CountAsync();

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return (items, totalRecords);
    }
}