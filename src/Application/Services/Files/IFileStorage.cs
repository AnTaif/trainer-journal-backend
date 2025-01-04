namespace TrainerJournal.Application.Services.Files;

public interface IFileStorage
{
    public Task<string> UploadAsync(Stream fileStream, string destinationDirectory, string destinationFile);

    public void Delete(string destinationDirectory, string destinationFile);
}