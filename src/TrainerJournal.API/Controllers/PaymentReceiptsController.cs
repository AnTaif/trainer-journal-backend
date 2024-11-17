using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.PaymentReceipts;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("receipts")]
[Authorize]
public class PaymentReceiptsController(
    IPaymentReceiptService paymentReceiptService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PaymentReceiptDto>> UploadPaymentReceiptAsync(
        IFormFile file, [FromForm] UploadPaymentReceiptRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var fileName = file.FileName;
        var stream = file.OpenReadStream();

        var errorOr = await paymentReceiptService.UploadAsync(Guid.Parse(userId), stream, fileName, request);
        return this.ToActionResult(errorOr, value => CreatedAtAction("UploadPaymentReceipt", value));
    }
}