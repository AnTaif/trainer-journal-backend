using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class ContactDto(string name, string phone, string relation)
{
    [Required]
    [DefaultValue("Имя Отчество")]
    public string Name { get; init; } = name;
    
    [Required]
    [DefaultValue("Папа")]
    public string Relation { get; init; } = relation;

    [Required]
    [Phone]
    [DefaultValue("+79995005050")]
    public string Phone { get; init; } = phone;
}