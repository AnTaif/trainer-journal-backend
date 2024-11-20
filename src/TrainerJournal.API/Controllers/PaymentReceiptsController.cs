using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.PaymentReceipts;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("receipts")]
[Authorize]
public class PaymentReceiptsController(
    IPaymentReceiptService paymentReceiptService) : ControllerBase
{
    private readonly string[] allowedFileExtensions = [".jpg", ".jpeg", ".png"];
    
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
    public async Task<ActionResult<List<PaymentReceiptDto>>> GetStudentsPaymentReceiptsAsync(
        string username, [FromQuery] bool? verified =  null)
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
        if (!allowedFileExtensions.Contains(Path.GetExtension(fileName)))
            return BadRequest("Invalid file extension. Allowed extensions: .jpg, .jpeg, .png");
            
        const long maxFileSize = 5 * 1024 * 1024;
        if (file.Length > maxFileSize)
        {
            return BadRequest($"The file size exceeds the allowed limit: {maxFileSize / (1024 * 1024)} MB");
        }
        
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

    [HttpPost("{id}/verify")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<PaymentReceiptDto>> VerifyPaymentReceiptAsync(Guid id, VerifyPaymentReceiptRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var errorOr = await paymentReceiptService.VerifyAsync(id, request);
        return this.ToActionResult(errorOr, Ok);
    }
}