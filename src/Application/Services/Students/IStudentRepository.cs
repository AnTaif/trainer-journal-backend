using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public interface IStudentRepository : IUnitOfWork
{
    Task<Student?> FindByUserIdAsync(Guid userId);

    Task<Student?> FindByUsernameAsync(string username);
    
    Task<Student?> FindByUsernameWithIncludesAsync(string username);

    Task<List<Student>> SelectByGroupIdAsync(Guid groupId);

    Task<List<Student>> SelectByTrainerIdAsync(Guid trainerId, bool withGroup);

    void AddStudent(Student student);

    Task UpdateContactsAsync(Student student, List<Contact>? contacts);
}