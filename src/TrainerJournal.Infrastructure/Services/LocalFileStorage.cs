using Microsoft.Extensions.Options;
using TrainerJournal.Application.Services;
using TrainerJournal.Domain.Options;

namespace TrainerJournal.Infrastructure.Services;

public class LocalFileStorage(IOptions<UploadOptions> options) : IFileStorage
{
    private UploadOptions uploadOptions => options.Value;
    
    public async Task<string> UploadAsync(Stream fileStream, string destinationDirectory, string destinationFile)
    {
        var destinationPath = Path.Combine(uploadOptions.UploadFilesPath, destinationDirectory, destinationFile);
        await using var stream = new FileStream(destinationPath, FileMode.Create);
        await fileStream.CopyToAsync(stream);

        return uploadOptions.UploadsUrl + "/" + destinationFile;
    }

    public void Delete(string destinationDirectory, string destinationFile)
    {
        var destinationPath = Path.Combine(uploadOptions.UploadFilesPath, destinationDirectory, destinationFile);
        
        if (!File.Exists(destinationPath))
            return;

        File.Delete(destinationPath);
    }
}