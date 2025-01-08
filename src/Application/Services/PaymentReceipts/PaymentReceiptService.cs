using TrainerJournal.Application.Services.BalanceChanges;
using TrainerJournal.Application.Services.Files;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;
using TrainerJournal.Domain.Common.Result;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public class PaymentReceiptService(
    IGroupRepository groupRepository,
    IBalanceChangeManager balanceChangeManager,
    IFileManager fileManager,
    IPaymentReceiptRepository paymentReceiptRepository) : IPaymentReceiptService
{
    public async Task<Result<PaymentReceiptDto>> GetByIdAsync(Guid id)
    {
        var paymentReceipt = await paymentReceiptRepository.FindByIdAsync(id);
        if (paymentReceipt == null) return Error.NotFound("Receipt not found");

        return paymentReceipt.ToDto();
    }

    public async Task<Result<List<PaymentReceiptDto>>> GetByStudentUsernameAsync(Guid userId, string username,
        bool? verified)
    {
        List<PaymentReceipt> paymentReceipts;

        if (verified == null)
            paymentReceipts = await paymentReceiptRepository.SelectByStudentUsernameAsync(username);
        else
            paymentReceipts =
                await paymentReceiptRepository.SelectByStudentUsernameAsync(username, verified.Value);

        return paymentReceipts.Select(p => p.ToDto()).ToList();
    }

    public async Task<Result<List<PaymentReceiptDto>>> GetByUserIdAsync(Guid userId, bool? verified)
    {
        List<PaymentReceipt> paymentReceipts;

        if (verified == null)
            paymentReceipts = await paymentReceiptRepository.SelectByUserIdAsync(userId);
        else
            paymentReceipts = await paymentReceiptRepository.SelectByUserIdAsync(userId, verified.Value);

        return paymentReceipts.Select(p => p.ToDto()).ToList();
    }

    public async Task<Result<List<PaymentReceiptDto>>> GetByGroupIdAsync(Guid groupId)
    {
        var group = await groupRepository.FindByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");

        var receipts = await paymentReceiptRepository.SelectByGroupIdAsync(groupId);

        return receipts.Select(r => r.ToDto()).ToList();
    }

    public async Task<Result<PaymentReceiptDto>> UploadAsync(Guid userId, Stream imageStream, string imageName,
        UploadPaymentReceiptRequest request)
    {
        var file = await fileManager.SavePublicFileAsync(imageStream, imageName, FileType.PaymentReceipt);
        
        var newPaymentReceipt = new PaymentReceipt(userId, request.Amount, file.Id, DateTime.UtcNow);
        
        paymentReceiptRepository.AddPaymentReceipt(newPaymentReceipt);
        await paymentReceiptRepository.SaveChangesAsync();

        var paymentReceipt = await paymentReceiptRepository.FindByIdAsync(newPaymentReceipt.Id);
        return paymentReceipt!.ToDto();
    }

    public async Task<Result<PaymentReceiptDto>> EditAsync(Guid secureUserId, Guid id, Stream? newImageStream, string? newImageName, float? newAmount)
    {
        var receipt = await paymentReceiptRepository.FindByIdAsync(id);
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
        var paymentReceipt = await paymentReceiptRepository.FindByIdAsync(id);
        if (paymentReceipt == null) return Error.NotFound("Receipt not found");

        fileManager.DeletePublicFile(paymentReceipt.Image);
        
        await paymentReceiptRepository.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<PaymentReceiptDto>> VerifyAsync(Guid id, VerifyPaymentReceiptRequest request)
    {
        var paymentReceipt = await paymentReceiptRepository.FindByIdAsync(id);
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