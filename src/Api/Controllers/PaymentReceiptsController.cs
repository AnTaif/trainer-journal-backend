using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.Api.Extensions;
using TrainerJournal.Application.Services.PaymentReceipts;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.Api.Controllers;

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

        var result = await paymentReceiptService.GetByIdAsync(id);
        return result.ToActionResult(this);
    }

    [HttpGet]
    public async Task<ActionResult<List<PaymentReceiptDto>>> GetPaymentReceiptsAsync([FromQuery] bool? verified = null)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await paymentReceiptService.GetByUserIdAsync(Guid.Parse(userId), verified);
        return result.ToActionResult(this);
    }

    [HttpGet("students/{username}")]
    public async Task<ActionResult<List<PaymentReceiptDto>>> GetStudentsPaymentReceiptsAsync(
        string username, [FromQuery] bool? verified = null)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await paymentReceiptService.GetByStudentUsernameAsync(Guid.Parse(userId), username, verified);
        return result.ToActionResult(this);
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
            return BadRequest($"The file size exceeds the allowed limit: {maxFileSize / (1024 * 1024)} MB");

        var stream = file.OpenReadStream();

        var result = await paymentReceiptService.UploadAsync(Guid.Parse(userId), stream, fileName, request);
        return result.ToActionResult(this,
            value => CreatedAtAction("UploadPaymentReceipt", value));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PaymentReceiptDto>> DeletePaymentReceiptAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await paymentReceiptService.DeleteAsync(Guid.Parse(userId), id);
        return result.ToActionResult(this);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PaymentReceiptDto>> EditPaymentReceiptAsync(
        Guid id, 
        IFormFile? file,
        [FromForm] EditPaymentReceiptRequest? request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        Stream? stream = null;
        string? fileName = null;

        if (file != null)
        {
            fileName = file.FileName;
            if (!allowedFileExtensions.Contains(Path.GetExtension(fileName)))
                return BadRequest("Invalid file extension. Allowed extensions: .jpg, .jpeg, .png");

            const long maxFileSize = 5 * 1024 * 1024;
            if (file.Length > maxFileSize)
                return BadRequest($"The file size exceeds the allowed limit: {maxFileSize / (1024 * 1024)} MB");

            stream = file.OpenReadStream();
        }

        var result = await paymentReceiptService.EditAsync(Guid.Parse(userId), id, stream, fileName, request?.Amount);
        return result.ToActionResult(this);
    }

    [HttpPost("{id}/verify")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<PaymentReceiptDto>> VerifyPaymentReceiptAsync(Guid id,
        VerifyPaymentReceiptRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await paymentReceiptService.VerifyAsync(id, request);
        return result.ToActionResult(this);
    }
}