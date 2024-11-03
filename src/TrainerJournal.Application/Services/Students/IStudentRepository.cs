using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public interface IStudentRepository
{
    public Task<Student?> GetByUserIdAsync(Guid userId);

    public Task<List<Student>> GetAllByGroupIdAsync(Guid groupId);

    public Task<List<Student>> GetAllByTrainerIdAsync(Guid trainerId, bool withGroup);

    public void AddStudent(Student student);

    public Task SaveChangesAsync();
}