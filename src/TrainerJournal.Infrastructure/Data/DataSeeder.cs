using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Infrastructure.Data;

public static class DataSeeder
{
    private static readonly Guid userAdminGuid = new("bb1e7f01-e532-470c-bda5-5be7b7e502dc");
    private static readonly Guid adminGuid = new("bb1e7f01-e532-470c-bda5-5be7b7e502dc");
    private static readonly Guid userTrainer1Guid = new("c21b2289-0537-42a6-ad1b-0fa76a25daa8");
    private static readonly Guid trainer1Guid = new("c21b2289-0537-42a6-ad1b-0fa76a25daa8");
    private static readonly Guid group1Guid = new("084d03ac-6192-4f03-abe3-cdab8aa7ad74");
    private static readonly Guid hall1Guid = new("16de890d-b54b-4365-865e-25e39be7c5b6");
    
    private const string password = "Password123"; // Get from environment
    
    public static void SeedOnModelCreating(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
    }

    public static async Task SeedOnMigratingAsync(UserManager<User> userManager, AppDbContext dbContext)
    {
        await SeedUsersAsync(userManager);
        await SeedTrainersAsync(dbContext);
        await SeedHallsAsync(dbContext);
        await SeedGroupsAsync(dbContext);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (await userManager.FindByNameAsync("admin") == null)
        {
            var adminUser = new User("admin", null, null, "admin user", Gender.Male)
            {
                Id = userAdminGuid
            };
            var result = await userManager.CreateAsync(adminUser, password);

            if (result.Succeeded)
                await userManager.AddToRolesAsync(adminUser, [RoleConstants.Admin, RoleConstants.Trainer]);
        }

        if (await userManager.FindByNameAsync("trainer1") == null)
        {
            var trainerUser = new User("trainer1", null, null, "Фамилия Имя Отчество", Gender.Male)
            {
                Id = userTrainer1Guid
            };
            var result = await userManager.CreateAsync(trainerUser, password);

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
    
    private static async Task SeedTrainersAsync(AppDbContext dbContext)
    {
        if (await dbContext.Trainers.FindAsync(adminGuid) == null)
        {
            var trainer1 = new Trainer(userAdminGuid)
            {
                Id = adminGuid
            };
            dbContext.Trainers.Add(trainer1);
        }
    }
    
    private static async Task SeedHallsAsync(AppDbContext dbContext)
    {
        if (await dbContext.Halls.FindAsync(hall1Guid) == null)
        {
            var hall1 = new Hall("Юго-Западный район", "Спортивный зал ЮЗ")
            {
                Id = hall1Guid
            };
            dbContext.Halls.Add(hall1);
        }
    }
    
    private static async Task SeedGroupsAsync(AppDbContext dbContext)
    {
        if (await dbContext.Groups.FindAsync(group1Guid) == null)
        {
            var group1 = new Group("Группа 1", adminGuid, hall1Guid)
            {
                Id = group1Guid
            };
            dbContext.Groups.Add(group1);
        }
    }
}