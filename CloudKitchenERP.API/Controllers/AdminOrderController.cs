using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Order;
using CloudKitchenERP.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminOrderController : ControllerBase
{
    private readonly IAdminOrderService _adminOrderService;

    public AdminOrderController(IAdminOrderService adminOrderService)
    {
        _adminOrderService = adminOrderService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _adminOrderService.GetAllOrdersAsync();
        return Ok(result);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(OrderStatus status)
    {
        var result = await _adminOrderService.GetOrdersByStatusAsync(status);
        return Ok(result);
    }

    [HttpPut("status")]
    public async Task<IActionResult> UpdateStatus(UpdateOrderStatusRequest request)
    {
        var success = await _adminOrderService.UpdateOrderStatusAsync(request);

        if (!success)
            return NotFound();

        return NoContent();
    }
}