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
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentReceiptDto>> GetByIdAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var errorOr = await paymentReceiptService.GetByIdAsync(id);
        return this.ToActionResult(errorOr, Ok);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<PaymentReceiptDto>>> GetPaymentReceiptsAsync([FromQuery] bool? verified =  null)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var errorOr = await paymentReceiptService.GetByUserIdAsync(Guid.Parse(userId), verified);
        return this.ToActionResult(errorOr, Ok);
    }
    
    [HttpGet("students/{username}")]
    public async Task<ActionResult<List<PaymentReceiptDto>>> GetStudentsPaymentReceiptsAsync(string username, [FromQuery] bool? verified =  null)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var errorOr = await paymentReceiptService.GetByStudentUsernameAsync(Guid.Parse(userId), username, verified);
        return this.ToActionResult(errorOr, Ok);
    }
    
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

    [HttpDelete("{id}")]
    public async Task<ActionResult<PaymentReceiptDto>> DeletePaymentReceiptAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var errorOr = await paymentReceiptService.DeleteAsync(Guid.Parse(userId), id);
        return this.ToActionResult(errorOr, _ => NoContent());
    }
}