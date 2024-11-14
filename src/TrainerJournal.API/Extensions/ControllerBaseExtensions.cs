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
                ErrorType.NotFound => controller.NotFound(error.Code),
                ErrorType.Forbidden => controller.Forbid(),
                _ => controller.BadRequest(error.Code)
            }
        );
    }
}