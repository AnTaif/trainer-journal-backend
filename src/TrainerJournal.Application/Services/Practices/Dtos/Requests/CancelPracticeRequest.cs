namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public class CancelPracticeRequest
{
    public DateTime PracticeStart { get; set; }
    
    public string? Comment { get; set; }
}