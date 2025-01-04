using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Domain.Common.Result;

namespace TrainerJournal.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<T>(
        this Result<T> result, ControllerBase thisController, Func<T, ActionResult>? onValue = null)
    {
        if (result.IsError()) return thisController.StatusCode((int)result.Error.StatusCode, result.Error.Message);

        return onValue?.Invoke(result.Value) ?? thisController.Ok(result.Value);
    }
    
    public static ActionResult ToActionResult(this Result result, ControllerBase thisController)
    {
        if (result.IsError()) return thisController.StatusCode((int)result.Error.StatusCode, result.Error.Message);

        return thisController.NoContent();
    }
}