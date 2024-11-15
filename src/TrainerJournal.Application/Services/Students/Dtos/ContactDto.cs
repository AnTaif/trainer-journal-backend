using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class ContactDto
{
    [Required]
    [DefaultValue("Имя Отчество")]
    public string Name { get; init; }
    
    [Required]
    [DefaultValue("Папа")]
    public string Relation { get; init; }

    [Required]
    [Phone]
    [DefaultValue("+79995005050")]
    public string Phone { get; init; }
}