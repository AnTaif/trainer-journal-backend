using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums;

namespace TrainerJournal.Domain.Entities;
    
public class SavedFile(string storageKey, string url, FileType fileType) : Entity<Guid>(Guid.NewGuid())
{
    public string StorageKey { get; private set; } = storageKey;

    public string Url { get; private set; } = url;
    
    public FileType FileType { get; private set; } = fileType;
}