using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums;

namespace TrainerJournal.Application.Services.Files;

public class FileManager(
    ISavedFileRepository savedFileRepository,
    IFileStorage fileStorage) : IFileManager
{
    private const string PublicDirectory = "Public";
    
    public async Task<SavedFile> SavePublicFileAsync(Stream fileStream, string fileName, FileType fileType, string? destFileName = null)
    {
        var destName = destFileName ?? Guid.NewGuid() + Path.GetExtension(fileName);
        var url = await fileStorage.UploadAsync(fileStream, PublicDirectory, destName);
        var file = new SavedFile(destName, url, fileType);
        
        savedFileRepository.Add(file);
        return file;
    }

    public void DeletePublicFile(SavedFile fileToDelete)
    {
        savedFileRepository.Remove(fileToDelete);
        fileStorage.Delete(PublicDirectory, fileToDelete.StorageKey);
    }
}