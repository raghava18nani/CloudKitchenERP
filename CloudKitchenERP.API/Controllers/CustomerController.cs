using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("User ID claim not found.");

        int userId = int.Parse(userIdClaim.Value);

        var customer = await _customerService.GetProfileAsync(userId);

        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("User ID claim not found.");

        int userId = int.Parse(userIdClaim.Value);
        var customer = await _customerService.CreateAsync(userId, request);

        return Ok(customer);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCustomerRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("User ID claim not found.");

        int userId = int.Parse(userIdClaim.Value);

        var success = await _customerService.UpdateAsync(userId, request);

        if (!success)
            return NotFound();

        return NoContent();
    }
}