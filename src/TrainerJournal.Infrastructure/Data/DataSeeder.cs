using Microsoft.AspNetCore.Identity;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Constants;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Infrastructure.Data;

public static class DataSeeder
{
    private static readonly Guid adminGuid = new("bb1e7f01-e532-470c-bda5-5be7b7e502dc");
    private static readonly Guid trainer1Guid = new("c21b2289-0537-42a6-ad1b-0fa76a25daa8");
    private static readonly Guid hall1Guid = new("16de890d-b54b-4365-865e-25e39be7c5b6");
    private static readonly Guid group1Guid = new("084d03ac-6192-4f03-abe3-cdab8aa7ad74");
    private static readonly Guid group2Guid = new("b491d9a7-4428-46f2-bf09-71dbfc31919d");
    
    private const string password = "Password123"; // Get from environment

    public static async Task SeedOnMigratingAsync(UserManager<User> userManager, AppDbContext dbContext)
    {
        if (await userManager.FindByNameAsync("admin") != null)
            return;
        
        await SeedUsersAsync(userManager);
        await SeedTrainersAsync(dbContext);
        await SeedHallsAsync(dbContext);
        await SeedGroupsAsync(dbContext);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        var adminUser = new User("admin", null, null, new PersonName("admin user"), Gender.Male)
        {
            Id = adminGuid
        };
        await AddUserAsync(userManager, adminUser, Roles.Admin, Roles.Trainer);

        var trainerUser = new User("D.S.Smirnov", null, null, new PersonName("Смирнов Денис Сергеевич"), Gender.Male)
        {
            Id = trainer1Guid
        };
        await AddUserAsync(userManager, trainerUser, Roles.Trainer);
    }

    private static async Task AddUserAsync(UserManager<User> userManager, User user, params string[] roles)
    {
        var adminResult = await userManager.CreateAsync(user, password);

        if (adminResult.Succeeded)
            await userManager.AddToRolesAsync(user, roles);
    }
    
    private static async Task SeedTrainersAsync(AppDbContext dbContext)
    {
        var admin = new Trainer(adminGuid) { Id = adminGuid };
        var trainer1 = new Trainer(trainer1Guid) { Id = trainer1Guid };
        
        await dbContext.Trainers.AddRangeAsync(admin, trainer1);
    }
    
    private static Task SeedGroupsAsync(AppDbContext dbContext)
    {
        var group1 = new Group("Группа 1", adminGuid, hall1Guid)
        {
            Id = group1Guid
        };

        var group2 = new Group("Младшая группа", trainer1Guid, hall1Guid)
        {
            Id = group2Guid
        };
        
        dbContext.Groups.AddRange(group1, group2);
        return Task.CompletedTask;
    }
    
    private static Task SeedHallsAsync(AppDbContext dbContext)
    {
        var hall1 = new Hall("Юго-Западный район", "Спортивный зал ЮЗ")
        {
            Id = hall1Guid
        };
        dbContext.Halls.Add(hall1);
        return Task.CompletedTask;
    }
}