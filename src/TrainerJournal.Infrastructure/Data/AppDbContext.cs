using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Trainer> Trainers { get; set; } = null!;
    
    public DbSet<Student> Students { get; set; } = null!;
    
    public DbSet<Group> Groups { get; set; } = null!;
    
    public DbSet<Hall> Halls { get; set; } = null!;
    
    public DbSet<Payment> Payments { get; set; } = null!;

    public DbSet<Visit> Visits { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DataSeeder.Seed(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}