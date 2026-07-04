using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Domain.Entities;
using CloudKitchenERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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
}