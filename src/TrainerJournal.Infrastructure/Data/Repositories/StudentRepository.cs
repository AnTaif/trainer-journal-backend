using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class StudentRepository(AppDbContext context) : BaseRepository(context), IStudentRepository
{
    private DbSet<Student> students => dbContext.Students;
    private DbSet<Contact> contacts => dbContext.Contacts;
    
    public async Task<Student?> GetByUserIdAsync(Guid userId)
    {
        return await students
            .Include(s => s.User)
            .Include(s => s.Groups)
            .Include(s => s.Contacts)
            .FirstOrDefaultAsync(student => student.Id == userId);
    }
    
    public async Task<Student?> GetByUsernameAsync(string username)
    {
        return await students
            .FirstOrDefaultAsync(student => student.User.UserName == username);
    }

    public async Task<Student?> GetByUsernameWithIncludesAsync(string username)
    {
        return await students
            .Include(s => s.User)
            .Include(s => s.Groups)
            .Include(s => s.Contacts)
            .FirstOrDefaultAsync(student => student.User.UserName == username);
    }

    public async Task<List<Student>> GetAllByGroupIdAsync(Guid groupId)
    {
        return await students
            .Include(s => s.User)
            .Include(s => s.Contacts)
            .Where(s => s.Groups.Any(g => g.Id == groupId))
            .ToListAsync();
    }

    public async Task<List<Student>> GetAllByTrainerIdAsync(Guid trainerId, bool withGroup)
    {
        var includableQuery = students
            .Include(s => s.User)
            .Include(s => s.Contacts)
            .Include(s => s.Groups);

        if (withGroup)
            return await includableQuery
                .Where(s => s.Groups.Any(g => g.TrainerId == trainerId))
                .ToListAsync();

        return await includableQuery
            .Where(s => s.Groups.Count == 0)
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