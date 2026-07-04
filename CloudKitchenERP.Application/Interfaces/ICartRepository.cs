using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(int userId);

    Task<CartItem?> GetCartItemAsync(int cartItemId);

    Task<CartItem?> GetCartItemByMenuItemAsync(int cartId, int menuItemId);

    Task AddCartAsync(Cart cart);

    Task AddCartItemAsync(CartItem item);

    Task UpdateCartItemAsync(CartItem item);

    Task DeleteCartItemAsync(CartItem item);

    Task SaveChangesAsync();
    Task<List<CartItem>> GetCartItemsAsync(int cartId);
}