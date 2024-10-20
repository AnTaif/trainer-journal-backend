using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data;

public static class DataSeeder
{
    public static void SeedOnModelCreating(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
    }

    public static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (await userManager.FindByNameAsync("admin") == null)
        {
            var adminUser = new User("admin", null, null, "admin user", Gender.Male);
            var result = await userManager.CreateAsync(adminUser, "Password123");

            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
        }

        if (await userManager.FindByNameAsync("trainer1") == null)
        {
            var trainerUser = new User("trainer1", null, null, "Фамилия Имя Отчество", Gender.Male);
            var result = await userManager.CreateAsync(trainerUser, "Password123");

            if (result.Succeeded)
                await userManager.AddToRoleAsync(trainerUser, RoleConstants.Trainer);
        }
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        var roles = new[] { RoleConstants.Admin, RoleConstants.Trainer, RoleConstants.User };
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