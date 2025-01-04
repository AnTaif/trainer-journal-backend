using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public class AddStudentRequest
{
    [Required] 
    [DefaultValue("login")] 
    public string StudentUsername { get; set; } = null!;
}