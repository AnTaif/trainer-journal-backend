using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class ExtraContactsRepository(AppDbContext dbContext) : IExtraContactsRepository
{
    private DbSet<ExtraContact> extraContacts => dbContext.ExtraContacts;
    
    public void RemoveRange(List<ExtraContact> extra)
    {
        extraContacts.RemoveRange(extra);
    }

    public void AddRange(List<ExtraContact> extra)
    {
        extraContacts.AddRange(extra);
    }
}