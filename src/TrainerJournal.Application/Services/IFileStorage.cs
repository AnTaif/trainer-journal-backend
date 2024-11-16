namespace TrainerJournal.Application.Services;

public interface IFileStorage
{
    public Task<string> UploadAsync(Stream fileStream, string destinationDirectory, string destinationFile);
}