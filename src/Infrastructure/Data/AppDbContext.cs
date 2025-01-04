using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Trainer> Trainers { get; set; } = null!;
    
    public DbSet<Student> Students { get; set; } = null!;

    public DbSet<Contact> Contacts { get; set; } = null!;
    
    public DbSet<Group> Groups { get; set; } = null!;
    
    public DbSet<PaymentReceipt> PaymentReceipts { get; set; } = null!;

    public DbSet<BalanceChange> BalanceChanges { get; set; } = null!;

    public DbSet<SavedFile> SavedFiles { get; set; } = null!;

    public DbSet<Schedule> Schedules { get; set; } = null!;

    public DbSet<Practice> Practices { get; set; } = null!;
    
    public DbSet<SchedulePractice> SchedulePractices { get; set; } = null!;
    
    public DbSet<SinglePractice> SinglePractices { get; set; } = null!;

    public DbSet<AttendanceMark> AttendanceMarks { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.ComplexProperty(e => e.FullName, e => e.IsRequired());
        });

        modelBuilder.Entity<Trainer>(b =>
        {
            b.HasKey(t => t.Id);
            b.HasOne(t => t.User);
        });

        modelBuilder.Entity<Student>(b =>
        {
            b.HasKey(s => s.Id);
            b.HasOne(s => s.User);
        });
        
        var roles = new[] { Domain.Constants.Roles.Admin, Domain.Constants.Roles.Trainer, Domain.Constants.Roles.User };
        var identityRoles = roles.Select(role => 
            new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(), 
                Name = role, 
                NormalizedName = role.ToUpper(), 
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

        modelBuilder.Entity<IdentityRole<Guid>>().HasData(identityRoles);
        
        base.OnModelCreating(modelBuilder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = new List<DomainEvent>();

        foreach (var entry in ChangeTracker.Entries<Entity<Guid>>())
        {
            domainEvents.AddRange(entry.Entity.DomainEvents);
            entry.Entity.ClearDomainEvents();
        }
        
        foreach (var entry in ChangeTracker.Entries<Entity<int>>())
        {
            domainEvents.AddRange(entry.Entity.DomainEvents);
            entry.Entity.ClearDomainEvents();
        }

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
        
        var result = await base.SaveChangesAsync(cancellationToken);

        //TODO: add notifications (events that invokes after transaction commit)
        
        return result;
    }
}