using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums;

namespace TrainerJournal.Application.Services.Files;

public interface IFileManager
{
    Task<SavedFile> SavePublicFileAsync(Stream fileStream, string fileName, FileType fileType, string? destFileName = null);

    void DeletePublicFile(SavedFile fileToDelete);
}