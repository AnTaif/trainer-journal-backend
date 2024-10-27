using ErrorOr;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;

namespace TrainerJournal.Application.Services.Practices;

public interface IPracticeService
{
    public Task<ErrorOr<PracticeInfoDto>> GetByIdAsync(Guid id, Guid userId);

    public Task<ErrorOr<List<PracticeItemDto>>> GetByGroupIdAsync(Guid groupId, Guid userId, DateTime startDate, int daysCount);

    public Task<ErrorOr<List<PracticeItemDto>>> GetByUserIdAsync(Guid userId, DateTime startDate, int daysCount);

    public Task<ErrorOr<List<PracticeItemDto>>> CreateScheduleAsync(
        CreateScheduleRequest request, Guid groupId, Guid userId);
}