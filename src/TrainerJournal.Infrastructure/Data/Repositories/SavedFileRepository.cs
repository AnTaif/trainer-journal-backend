using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services;
using TrainerJournal.Application.Services.Files;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class SavedFileRepository(AppDbContext context) : BaseRepository(context), ISavedFileRepository
{
    private DbSet<SavedFile> savedFiles => dbContext.SavedFiles;
    
    public void Add(SavedFile savedFile)
    {
        savedFiles.Add(savedFile);
    }

    public void Remove(SavedFile savedFile)
    {
        savedFiles.Remove(savedFile);
    }
}