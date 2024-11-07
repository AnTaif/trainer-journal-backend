using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class StudentRepository(AppDbContext dbContext) : IStudentRepository
{
    private DbSet<Student> students => dbContext.Students;
    
    public async Task<Student?> GetByUserIdAsync(Guid userId)
    {
        return await students
            .Include(s => s.User)
            .Include(s => s.Group)
            .Include(s => s.ExtraContacts)
            .FirstOrDefaultAsync(student => student.UserId == userId);
    }

    public async Task<List<Student>> GetAllByGroupIdAsync(Guid groupId)
    {
        return await students
            .Include(s => s.User)
            .Include(s => s.ExtraContacts)
            .Where(s => s.GroupId == groupId)
            .ToListAsync();
    }

    public async Task<List<Student>> GetAllByTrainerIdAsync(Guid trainerId, bool withGroup)
    {
        var includableQuery = students
            .Include(s => s.User)
            .Include(s => s.ExtraContacts)
            .Include(s => s.Group);

        if (withGroup)
            return await includableQuery
                .Where(s => s.GroupId != null && s.Group!.TrainerId == trainerId)
                .ToListAsync();
        
        return await includableQuery
            .Where(s => s.GroupId == null)
            .ToListAsync();
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public Task SaveChangesAsync() => dbContext.SaveChangesAsync();
}