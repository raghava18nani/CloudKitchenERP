using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    private int GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (claim == null)
            throw new UnauthorizedAccessException("User ID claim not found.");

        return int.Parse(claim.Value);
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(CheckoutRequest request)
    {
        var result = await _orderService.CheckoutAsync(GetUserId(), request);
        return Ok(result);
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var result = await _orderService.GetMyOrdersAsync(GetUserId());
        return Ok(result);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetById(int orderId)
    {
        var result = await _orderService.GetByIdAsync(GetUserId(), orderId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}