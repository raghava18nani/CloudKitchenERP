using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Domain.Entities;
using CloudKitchenERP.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CloudKitchenERP.Persistence.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetCartByUserIdAsync(int userId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(i => i.MenuItem)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<CartItem?> GetCartItemAsync(int cartItemId)
    {
        return await _context.CartItems
            .Include(c => c.MenuItem)
            .FirstOrDefaultAsync(c => c.Id == cartItemId);
    }

    public async Task<CartItem?> GetCartItemByMenuItemAsync(int cartId, int menuItemId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(x =>
                x.CartId == cartId &&
                x.MenuItemId == menuItemId);
    }

    public async Task AddCartAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
    }

    public async Task AddCartItemAsync(CartItem item)
    {
        await _context.CartItems.AddAsync(item);
    }

    public Task UpdateCartItemAsync(CartItem item)
    {
        _context.CartItems.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteCartItemAsync(CartItem item)
    {
        _context.CartItems.Remove(item);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
   
    public async Task<List<CartItem>> GetCartItemsAsync(int cartId)
    {
        return await _context.CartItems
            .Include(x => x.MenuItem)
            .Where(x => x.CartId == cartId)
            .ToListAsync();
    }

    public async Task ClearCartAsync(int cartId)
    {
        var items = await _context.CartItems
            .Where(x => x.CartId == cartId)
            .ToListAsync();

        _context.CartItems.RemoveRange(items);
    }
}