using TrainerJournal.Application.Services.Trainers.Dtos.Requests;
using TrainerJournal.Application.Services.Users;
using TrainerJournal.Application.Services.Users.Dtos.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Services.Trainers;

public class TrainerService(
    ITrainerRepository trainerRepository,
    IUserService userService) : ITrainerService
{
    public async Task<Result<RegisterUserResponse>> RegisterTrainerAsync(RegisterTrainerRequest request)
    {
        var userResult = await userService.CreateAsync(request.FullName, request.Gender.ToGenderEnum());
        if (userResult.IsError()) return userResult.Error;
        var user = userResult.Value;

        var trainer = new Trainer(user.Id, request.Phone, request.Email);
        await trainerRepository.AddAsync(trainer);
        await trainerRepository.SaveChangesAsync();

        return new RegisterUserResponse
        {
            Username = user.UserName!,
            Password = user.Password,
            FullName = user.FullName.ToString()
        };
    }
}