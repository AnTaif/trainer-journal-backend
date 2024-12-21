using TrainerJournal.Application.Services.BalanceChanges;
using TrainerJournal.Application.Services.Files;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;
using TrainerJournal.Domain.Common.Result;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public class PaymentReceiptService(
    IBalanceChangeManager balanceChangeManager,
    IFileManager fileManager,
    IPaymentReceiptRepository paymentReceiptRepository) : IPaymentReceiptService
{
    public async Task<Result<PaymentReceiptDto>> GetByIdAsync(Guid id)
    {
        var paymentReceipt = await paymentReceiptRepository.GetByIdAsync(id);
        if (paymentReceipt == null) return Error.NotFound("Receipt not found");

        return paymentReceipt.ToDto();
    }

    public async Task<Result<List<PaymentReceiptDto>>> GetByStudentUsernameAsync(Guid userId, string username,
        bool? verified)
    {
        List<PaymentReceipt> paymentReceipts;

        if (verified == null)
            paymentReceipts = await paymentReceiptRepository.GetByStudentUsernameAsync(username);
        else
            paymentReceipts =
                await paymentReceiptRepository.GetVerifiedByStudentUsernameAsync(username, verified.Value);

        return paymentReceipts.Select(p => p.ToDto()).ToList();
    }

    public async Task<Result<List<PaymentReceiptDto>>> GetByUserIdAsync(Guid userId, bool? verified)
    {
        List<PaymentReceipt> paymentReceipts;

        if (verified == null)
            paymentReceipts = await paymentReceiptRepository.GetAllByUserIdAsync(userId);
        else
            paymentReceipts = await paymentReceiptRepository.GetVerifiedByUserIdAsync(userId, verified.Value);

        return paymentReceipts.Select(p => p.ToDto()).ToList();
    }

    public async Task<Result<PaymentReceiptDto>> UploadAsync(Guid userId, Stream imageStream, string imageName,
        UploadPaymentReceiptRequest request)
    {
        var file = await fileManager.SavePublicFileAsync(imageStream, imageName, FileType.PaymentReceipt);
        
        var newPaymentReceipt = new PaymentReceipt(userId, request.Amount, file.Id, DateTime.UtcNow);
        
        paymentReceiptRepository.AddPaymentReceipt(newPaymentReceipt);
        await paymentReceiptRepository.SaveChangesAsync();

        var paymentReceipt = await paymentReceiptRepository.GetByIdAsync(newPaymentReceipt.Id);
        return paymentReceipt!.ToDto();
    }

    public async Task<Result<PaymentReceiptDto>> EditAsync(Guid secureUserId, Guid id, Stream? newImageStream, string? newImageName, float? newAmount)
    {
        var receipt = await paymentReceiptRepository.GetByIdAsync(id);
        if (receipt == null) return Error.NotFound("Receipt not found");

        if (newImageStream != null)
        {
            await HandleEditReceiptImageAsync(receipt, newImageStream, newImageName!);
        }

        if (newAmount != null)
        {
            receipt.EditAmount(newAmount.Value);
        }

        await paymentReceiptRepository.SaveChangesAsync();
        return receipt.ToDto();
    }

    private async Task HandleEditReceiptImageAsync(PaymentReceipt receipt, Stream image, string imageName)
    {
        var fileToDelete = receipt.Image;
        var file = await fileManager.SavePublicFileAsync(image, imageName, FileType.PaymentReceipt);
        
        receipt.ChangeImage(file.Id);
        
        fileManager.DeletePublicFile(fileToDelete);
    }

    public async Task<Result> DeleteAsync(Guid userId, Guid id)
    {
        var paymentReceipt = await paymentReceiptRepository.GetByIdAsync(id);
        if (paymentReceipt == null) return Error.NotFound("Receipt not found");

        fileManager.DeletePublicFile(paymentReceipt.Image);
        
        await paymentReceiptRepository.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<PaymentReceiptDto>> VerifyAsync(Guid id, VerifyPaymentReceiptRequest request)
    {
        var paymentReceipt = await paymentReceiptRepository.GetByIdAsync(id);
        if (paymentReceipt == null) return Error.NotFound("Receipt not found");
        
        if (request.IsAccepted)
        {
            paymentReceipt.Accept();
            await balanceChangeManager.ChangeBalanceAsync(paymentReceipt.Student, paymentReceipt.Amount,
                BalanceChangeReason.Payment);
        }
        else
        {
            paymentReceipt.Decline(request.DeclineComment ?? "");
            await balanceChangeManager.ChangeBalanceAsync(paymentReceipt.Student, -paymentReceipt.Amount,
                BalanceChangeReason.PaymentRejection);
        }
        
        await paymentReceiptRepository.SaveChangesAsync();

        return paymentReceipt.ToDto();
    }
}