using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authenticationService.RegisterAsync(request);

        if (!result.Success)
        {
            return Conflict(result); // HTTP 409
        }

        return Ok(result); // HTTP 200
    }
}
