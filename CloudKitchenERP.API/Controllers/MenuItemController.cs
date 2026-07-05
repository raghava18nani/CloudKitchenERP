using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.MenuItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/upload-image")]
    public async Task<IActionResult> UploadImage(
    int id,
    IFormFile image)
    {
        if (image == null || image.Length == 0)
            return BadRequest("Please select an image.");

        // Allow only image files
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

        var extension = Path.GetExtension(image.FileName).ToLower();

        if (!allowedExtensions.Contains(extension))
            return BadRequest("Only JPG, JPEG, PNG and WEBP images are allowed.");

        // Maximum 5 MB
        if (image.Length > 5 * 1024 * 1024)
            return BadRequest("Image size cannot exceed 5 MB.");

        var fileName = $"{Guid.NewGuid()}{extension}";

        var folder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "images",
            "menu");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var filePath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        var imageUrl = $"/images/menu/{fileName}";

        var success = await _menuItemService.UpdateImageUrlAsync(id, imageUrl);

        if (!success)
            return NotFound("Menu item not found.");

        return Ok(new
        {
            ImageUrl = imageUrl
        });
    }
}