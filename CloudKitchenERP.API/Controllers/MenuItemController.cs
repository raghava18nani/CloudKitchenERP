using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.MenuItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    // Everyone can view menu
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _menuItemService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _menuItemService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // Only Admin
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuItemRequest request)
    {
        var result = await _menuItemService.CreateAsync(request);
        return Ok(result);
    }

    // Only Admin
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateMenuItemRequest request)
    {
        if (id != request.Id)
            return BadRequest("Id mismatch.");

        var success = await _menuItemService.UpdateAsync(request);

        if (!success)
            return NotFound();

        return NoContent();
    }

    // Only Admin
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _menuItemService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}