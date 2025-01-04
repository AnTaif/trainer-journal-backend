using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Files;

public interface ISavedFileRepository : IUnitOfWork
{
    void Add(SavedFile savedFile);

    void Remove(SavedFile savedFile);
}