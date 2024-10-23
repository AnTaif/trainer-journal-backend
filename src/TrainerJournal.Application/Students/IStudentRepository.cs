using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Students;

public interface IStudentRepository
{
    public Task<Student?> GetByUserIdAsync(Guid userId);

    public Task<List<Student>> GetAllByGroupIdAsync(Guid groupId);

    public void AddStudent(Student student);

    public Task SaveChangesAsync();
}