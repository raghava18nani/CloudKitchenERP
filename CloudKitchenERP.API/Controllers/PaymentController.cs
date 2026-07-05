using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Contracts.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("pay")]
    public async Task<IActionResult> Pay(PayOrderRequest request)
    {
        int userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _paymentService.PayAsync(userId, request);

        if (result == null)
            return NotFound("Order not found.");

        return Ok(result);
    }

    [HttpGet("history")]
    public async Task<IActionResult> History()
    {
        int userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _paymentService.GetPaymentHistoryAsync(userId);

        return Ok(result);
    }
}