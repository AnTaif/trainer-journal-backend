using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace TrainerJournal.API.Extensions;

public static class ControllerBaseExtensions
{
    public static ActionResult ToActionResult<T>(
        this ControllerBase controller, ErrorOr<T> result, Func<T, ActionResult> onValue)
    {
        return result.MatchFirst(
            onValue: onValue,
            onFirstError: error => error.Type switch
            {
                ErrorType.NotFound => controller.NotFound(error.Description),
                ErrorType.Forbidden => controller.Forbid(error.Description),
                _ => controller.BadRequest(error.Description)
            }
        );
    }
}