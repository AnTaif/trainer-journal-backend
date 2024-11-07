using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Trainer> Trainers { get; set; } = null!;
    
    public DbSet<Student> Students { get; set; } = null!;

    public DbSet<ExtraContact> ExtraContacts { get; set; } = null!;
    
    public DbSet<Group> Groups { get; set; } = null!;
    
    public DbSet<Hall> Halls { get; set; } = null!;
    
    public DbSet<Payment> Payments { get; set; } = null!;

    public DbSet<Schedule> Schedules { get; set; } = null!;

    public DbSet<Practice> Practices { get; set; } = null!;
    
    public DbSet<SchedulePractice> SchedulePractices { get; set; } = null!;
    
    public DbSet<SinglePractice> SinglePractices { get; set; } = null!;

    public DbSet<Visit> Visits { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.ComplexProperty(e => e.FullName, e => e.IsRequired());
        });

        modelBuilder.Entity<Trainer>(b =>
        {
            b.HasKey(t => t.UserId);
            b.HasOne(t => t.User);
        });

        modelBuilder.Entity<Student>(b =>
        {
            b.HasKey(s => s.UserId);
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
}