using TrainerJournal.Application.Services.Trainers.Dtos.Requests;
using TrainerJournal.Application.Services.Users.Dtos.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Common.Result;

namespace TrainerJournal.Application.Services.Trainers;

public interface ITrainerService
{
    Task<Result<RegisterUserResponse>> RegisterTrainerAsync(RegisterTrainerRequest request);
}