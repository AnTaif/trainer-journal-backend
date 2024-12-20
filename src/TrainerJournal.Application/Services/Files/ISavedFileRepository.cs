using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Files;

public interface ISavedFileRepository
{
    public void Add(SavedFile savedFile);

    public void Remove(SavedFile savedFile);

    public Task SaveChangesAsync();
}