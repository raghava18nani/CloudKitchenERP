using CloudKitchenERP.Contracts.Cart;

namespace CloudKitchenERP.Application.Interfaces;

public interface ICartService
{
    Task<CartResponse> GetCartAsync(int userId);

    Task<CartResponse> AddToCartAsync(int userId, AddToCartRequest request);

    Task<bool> UpdateQuantityAsync(int userId, UpdateCartItemRequest request);

    Task<bool> RemoveItemAsync(int userId, int cartItemId);

    Task<bool> ClearCartAsync(int userId);
}