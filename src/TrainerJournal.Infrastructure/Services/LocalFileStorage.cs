using Microsoft.Extensions.Options;
using TrainerJournal.Application.Services;
using TrainerJournal.Domain.Options;

namespace TrainerJournal.Infrastructure.Services;

public class LocalFileStorage(IOptions<UploadOptions> uploadOptions) : IFileStorage
{
    public async Task<string> UploadAsync(Stream fileStream, string destinationDirectory, string destinationFile)
    {
        var destinationPath = Path.Combine(uploadOptions.Value.UploadFilesPath, destinationDirectory, destinationFile);
        await using var stream = new FileStream(destinationPath, FileMode.Create);
        await fileStream.CopyToAsync(stream);

        return uploadOptions.Value.UploadsUrl + "/" + destinationFile;
    }
}