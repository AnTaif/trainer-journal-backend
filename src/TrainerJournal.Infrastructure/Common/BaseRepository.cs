using TrainerJournal.Infrastructure.Data;

namespace TrainerJournal.Infrastructure.Common;

public abstract class BaseRepository(AppDbContext context)
{
    protected readonly AppDbContext dbContext = context;
    
    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}