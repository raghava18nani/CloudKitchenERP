using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Cart;
using CloudKitchenERP.Domain.Entities;

namespace CloudKitchenERP.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMenuItemRepository _menuItemRepository;

    public CartService(
        ICartRepository cartRepository,
        IMenuItemRepository menuItemRepository)
    {
        _cartRepository = cartRepository;
        _menuItemRepository = menuItemRepository;
    }

    public async Task<CartResponse> GetCartAsync(int userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cart == null)
        {
            return new CartResponse
            {
                CartId = 0,
                Items = new List<CartItemResponse>(),
                GrandTotal = 0
            };
        }

        return new CartResponse
        {
            CartId = cart.Id,
            Items = cart.CartItems.Select(x => new CartItemResponse
            {
                CartItemId = x.Id,
                MenuItemId = x.MenuItemId,
                MenuItemName = x.MenuItem.Name,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice
            }).ToList(),
            GrandTotal = cart.CartItems.Sum(x => x.TotalPrice)
        };
    }

    // We'll implement these next
    public async Task<CartResponse> AddToCartAsync(int userId, AddToCartRequest request)
    {
        // Check whether the menu item exists
        var menuItem = await _menuItemRepository.GetByIdAsync(request.MenuItemId);

        if (menuItem == null)
            throw new Exception("Menu item not found.");

        // Get existing cart
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        // Create cart if it doesn't exist
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId
            };

            await _cartRepository.AddCartAsync(cart);
            await _cartRepository.SaveChangesAsync();
        }

        // Check if item already exists in cart
        var cartItem = await _cartRepository.GetCartItemByMenuItemAsync(cart.Id, request.MenuItemId);

        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                CartId = cart.Id,
                MenuItemId = menuItem.Id,
                Quantity = request.Quantity,
                UnitPrice = menuItem.Price,
                TotalPrice = menuItem.Price * request.Quantity
            };

            await _cartRepository.AddCartItemAsync(cartItem);
        }
        else
        {
            cartItem.Quantity += request.Quantity;
            cartItem.TotalPrice = cartItem.Quantity * cartItem.UnitPrice;

            await _cartRepository.UpdateCartItemAsync(cartItem);
        }

        await _cartRepository.SaveChangesAsync();

        return await GetCartAsync(userId);
    }

    public async Task<bool> UpdateQuantityAsync(int userId, UpdateCartItemRequest request)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cart == null)
            return false;

        var cartItem = await _cartRepository.GetCartItemAsync(request.CartItemId);

        if (cartItem == null || cartItem.CartId != cart.Id)
            return false;

        cartItem.Quantity = request.Quantity;
        cartItem.TotalPrice = cartItem.UnitPrice * request.Quantity;

        await _cartRepository.UpdateCartItemAsync(cartItem);
        await _cartRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveItemAsync(int userId, int cartItemId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cart == null)
            return false;

        var cartItem = await _cartRepository.GetCartItemAsync(cartItemId);

        if (cartItem == null || cartItem.CartId != cart.Id)
            return false;

        await _cartRepository.DeleteCartItemAsync(cartItem);
        await _cartRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ClearCartAsync(int userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cart == null)
            return false;

        var items = await _cartRepository.GetCartItemsAsync(cart.Id);

        foreach (var item in items)
        {
            await _cartRepository.DeleteCartItemAsync(item);
        }

        await _cartRepository.SaveChangesAsync();

        return true;
    }
}