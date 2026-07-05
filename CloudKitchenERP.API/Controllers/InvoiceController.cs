using CloudKitchenERP.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudKitchenERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> Download(int orderId)
    {
        int userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var pdf = await _invoiceService.GenerateInvoiceAsync(userId, orderId);

        return File(
            pdf,
            "application/pdf",
            $"Invoice-{orderId}.pdf");
    }
}