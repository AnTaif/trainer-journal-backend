using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Practices;

public interface IPracticeManager
{
    /// <summary>
    /// Возвращает SinglePractice по id; либо базовую SchedulePractice по id,
    /// проверяя при этом валидность запрашиваемого времени
    /// </summary>
    public Task<Result<Practice>> GetBasePracticeAsync(Guid id, DateTime time);

    public Task<Result<Practice>> UpdateSpecificPracticeAsync(Guid practiceId, DateTime practiceStart, Guid? groupId, 
        DateTime? newStart, DateTime? newEnd, string? hallAddress, string? practiceType, float? price);

    public Task<Result<Practice>> CancelSpecificPracticeAsync(Guid practiceId, DateTime practiceStart, string comment);
}