using Microsoft.AspNetCore.Identity;
using TrainerJournal.Application.Services.Users;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Constants;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Infrastructure.Data;

public static class DataSeeder
{
    private static readonly Guid adminGuid = new("bb1e7f01-e532-470c-bda5-5be7b7e502dc");
    private static readonly Guid trainer1Guid = new("c21b2289-0537-42a6-ad1b-0fa76a25daa8");
    private static readonly Guid student1Guid = new("8c034203-3551-4890-8d5f-b3c0d74f5290");
    private static readonly Guid student2Guid = new("0f3b8b3e-388d-4656-961d-39135b0d1287");
    private static readonly Guid student3Guid = new("58f23d91-89aa-4fa2-a7da-df1c931722a8");
    private static readonly Guid student4Guid = new("ee1cbfa5-2167-4f46-a0e8-98cf4ba460e8");
    private static readonly Guid group1Guid = new("084d03ac-6192-4f03-abe3-cdab8aa7ad74");
    private static readonly Guid group2Guid = new("b491d9a7-4428-46f2-bf09-71dbfc31919d");
    
    private const string password = "Password123"; // Get from environment

    public static async Task SeedOnMigratingAsync(UserManager<User> userManager, IUserService userService, AppDbContext dbContext)
    {
        if (await userManager.FindByNameAsync("admin") != null)
            return;

        await SeedEntities(userManager, userService, dbContext);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedEntities(UserManager<User> userManager, IUserService userService, AppDbContext dbContext)
    {
        // SEED USERS
        
        var adminUser = new User("admin", password, new PersonName("admin user"), Gender.Male)
        {
            Id = adminGuid
        };
        await AddUserAsync(userManager, adminUser, Roles.Admin, Roles.Trainer);

        var trainerUser = new User("D.S.Smirnov", password, new PersonName("Смирнов Денис Сергеевич"), Gender.Male)
        {
            Id = trainer1Guid
        };
        await AddUserAsync(userManager, trainerUser, Roles.Admin, Roles.Trainer);

        const string student1Fullname = "Юдин Андрей Никитич";
        var student1User = new User(userService.GenerateUsername(student1Fullname), password, new PersonName(student1Fullname), Gender.Male)
        {
            Id = student1Guid
        };
        await AddUserAsync(userManager, student1User, Roles.User);

        const string student2Fullname = "Масленникова Виктория Дмитриевна";
        var student2User = new User(userService.GenerateUsername(student2Fullname), password, new PersonName(student2Fullname), Gender.Female)
        {
            Id = student2Guid
        };
        await AddUserAsync(userManager, student2User, Roles.User);
        
        const string student3Fullname = "Смирнов Никита Давидович";
        var student3User = new User(userService.GenerateUsername(student3Fullname), password, new PersonName(student3Fullname), Gender.Male)
        {
            Id = student3Guid
        };
        await AddUserAsync(userManager, student3User, Roles.User);
        
        const string student4Fullname = "Чернышева София Романовна";
        var student4User = new User(userService.GenerateUsername(student4Fullname), password, new PersonName(student4Fullname), Gender.Female)
        {
            Id = student4Guid
        };
        await AddUserAsync(userManager, student4User, Roles.User);
        
        // SEED TRAINERS
        
        var admin = new Trainer(adminGuid);
        var trainer1 = new Trainer(trainer1Guid);
        
        await dbContext.Trainers.AddRangeAsync(admin, trainer1);
        
        // SEED STUDENTS
        
        var student1 = new Student(student1Guid, DateTime.Parse("2010-12-31").ToUniversalTime(), 7, 8, "Денисова-Уральского 5a",
        [
            new Contact("Ольга Николаевна", "Мама", "+79991001010"),
            new Contact("Никита Олегович", "Папа", "+79992002020")
        ]);
        
        var student2 = new Student(student2Guid, DateTime.Parse("2011-08-21").ToUniversalTime(), 6, 9, "Фонвизина 8",
        [
            new Contact("Виктория Андреевна", "Мама", "+79995005050"),
            new Contact("Дмитрий Викторович", "Папа", "+79996006060")
        ]);
        
        var student3 = new Student(student3Guid, DateTime.Parse("2010-03-09").ToUniversalTime(), 8, 6, "Коминтерна 5",
        [
            new Contact("Наталья Дмитриевна", "Мама", "+79997007070"),
            new Contact("Давид Владиславович", "Папа", "+79998008080")
        ]);
        
        var student4 = new Student(student4Guid, DateTime.Parse("2011-06-18").ToUniversalTime(), 7, 7, "Мира 32",
        [
            new Contact("Дарья Григорьевна", "Мама", "+79993003030"),
            new Contact("Роман Александрович", "Папа", "+79994004040")
        ]);

        await dbContext.Students.AddRangeAsync(student1, student2, student3, student4);
        
        // SEED GROUPS
        
        var group1 = new Group("Взрослая группа", 400, "ул. Академика Постовского, 11", new HexColor("#A293FF"), 
            trainer1Guid)
        {
            Id = group1Guid
        };

        var group2 = new Group("Младшая группа", 300, "ул. Академика Постовского, 11", new HexColor("#93FFCB"), 
            trainer1Guid)
        {
            Id = group2Guid
        };
        
        dbContext.Groups.AddRange(group1, group2);
        
        // SEED GROUP-STUDENT
        
        student1.AddToGroup(group1);
        student2.AddToGroup(group1);
        student3.AddToGroup(group1);
        student4.AddToGroup(group1);
    }

    private static async Task AddUserAsync(UserManager<User> userManager, User user, params string[] roles)
    {
        var adminResult = await userManager.CreateAsync(user, password);

        if (adminResult.Succeeded)
            await userManager.AddToRolesAsync(user, roles);
    }
}