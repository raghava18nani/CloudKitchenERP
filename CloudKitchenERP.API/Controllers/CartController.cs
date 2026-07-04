using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    private int GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (claim == null)
            throw new UnauthorizedAccessException("User ID claim not found.");

        return int.Parse(claim.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var result = await _cartService.GetCartAsync(GetUserId());
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddToCartRequest request)
    {
        var result = await _cartService.AddToCartAsync(GetUserId(), request);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateCartItemRequest request)
    {
        var success = await _cartService.UpdateQuantityAsync(GetUserId(), request);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("remove/{cartItemId}")]
    public async Task<IActionResult> Remove(int cartItemId)
    {
        var success = await _cartService.RemoveItemAsync(GetUserId(), cartItemId);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> Clear()
    {
        var success = await _cartService.ClearCartAsync(GetUserId());

        if (!success)
            return NotFound();

        return NoContent();
    }
}