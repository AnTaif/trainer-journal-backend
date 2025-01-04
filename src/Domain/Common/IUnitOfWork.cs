namespace TrainerJournal.Domain.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}