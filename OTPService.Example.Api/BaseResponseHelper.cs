using OTPService.Example.Models;

namespace OTPService.Example.Api;

public static class BaseResponseHelper
{
    public static IResult Execute<T>(Result<T> model)
    {
        if (model.IsValidationError)
        {
            return Results.BadRequest(model);
        }

        if (model.IsNotFoundError)
        {
            return Results.NotFound(model);
        }

        if (model.IsError)
        {
            return Results.Json(model, statusCode: 500);
        }

        return Results.Ok(model);
    }
}
