using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Infrastructure.Data;

public static class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        var roles = new[] { RoleConstants.Admin, RoleConstants.Trainer, RoleConstants.Student };
        var identityRoles = roles.Select(role => 
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(), 
                    Name = role, 
                    NormalizedName = role.ToUpper(), 
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });

        modelBuilder.Entity<IdentityRole<Guid>>().HasData(identityRoles);
    }
}