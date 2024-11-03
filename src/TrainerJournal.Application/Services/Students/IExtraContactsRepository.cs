using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public interface IExtraContactsRepository
{
    public void RemoveRange(List<ExtraContact> extra);

    public void AddRange(List<ExtraContact> extra);
}