using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Students;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class StudentRepository(AppDbContext dbContext) : IStudentRepository
{
    private DbSet<Student> students => dbContext.Students;
    
    public async Task<Student?> GetByUserIdAsync(Guid userId)
    {
        return await students
            .FirstOrDefaultAsync(student => student.UserId == userId);
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public Task SaveChangesAsync() => dbContext.SaveChangesAsync();
}