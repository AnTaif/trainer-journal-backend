using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public class CreateExtraContactRequest(string name, string contact)
{
    [Required]
    [DefaultValue("Имя Отчество")]
    public string Name { get; init; } = name;
    
    [Required]
    [Phone]
    [DefaultValue("+79995005050")]
    public string Contact { get; init; } = contact;
}