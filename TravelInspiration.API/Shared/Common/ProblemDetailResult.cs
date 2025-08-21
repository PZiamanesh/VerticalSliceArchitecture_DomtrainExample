namespace TravelInspiration.API.Shared.Common;

public static class ProblemDetailResult
{
    public static IResult ProblemDetail(IMessageResponse response)
    {
        if (response.HasValidationError)
        {
            return Results.ValidationProblem(errors: response.ValidationErrors, statusCode: response.StatusCode);
        }
        else if (response.HasLogicError)
        {
            return Results.Problem(detail: response.LogicError, statusCode: response.StatusCode);
        }
        else
        {
            return Results.Problem(detail: "An unexpected error has occoured.", statusCode: 500);
        }
    }
}
