using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class StudentRepository(AppDbContext context) : BaseRepository(context), IStudentRepository
{
    private DbSet<Student> students => dbContext.Students;
    private DbSet<Contact> contacts => dbContext.Contacts;
    
    public async Task<Student?> FindByUserIdAsync(Guid userId)
    {
        return await students
            .Include(s => s.User)
            .Include(s => s.Groups)
            .Include(s => s.Contacts)
            .FirstOrDefaultAsync(student => student.Id == userId);
    }
    
    public async Task<Student?> FindByUsernameAsync(string username)
    {
        return await students
            .FirstOrDefaultAsync(student => student.User.UserName == username);
    }

    public async Task<Student?> FindByUsernameWithIncludesAsync(string username)
    {
        return await students
            .Include(s => s.User)
            .Include(s => s.Groups)
            .Include(s => s.Contacts)
            .FirstOrDefaultAsync(student => student.User.UserName == username);
    }

    public async Task<List<Student>> SelectByGroupIdAsync(Guid groupId)
    {
        return await students
            .Include(s => s.User)
            .Include(s => s.Contacts)
            .Where(s => s.Groups.Any(g => g.Id == groupId))
            .OrderBy(s => s.User.FullName.ToString())
            .ToListAsync();
    }

    public async Task<List<Student>> SelectByTrainerIdAsync(Guid trainerId, bool withGroup)
    {
        var includableQuery = students
            .Include(s => s.User)
            .Include(s => s.Contacts)
            .Include(s => s.Groups)
            .AsQueryable();

        if (withGroup)
        {
            includableQuery = includableQuery
                .Where(s => s.Groups.Any(g => g.TrainerId == trainerId));
        }
        else
        {
            includableQuery = includableQuery
                .Where(s => s.Groups.Count == 0);
        }
        
        return await includableQuery
            .OrderBy(s => s.User.FullName.ToString())
            .ToListAsync();
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public async Task UpdateContactsAsync(Student student, List<Contact>? newContacts)
    {
        if (newContacts == null) return;
        
        contacts.RemoveRange(student.Contacts);
        student.UpdateContacts(newContacts);
        await contacts.AddRangeAsync(newContacts);
    }
}