using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Presentation.Endpoints;

public static class CustomResults
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException();

        return Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.Type),
            statusCode: GetStatusCode(result.Error.Type),
            extensions: GetErrors(result)
        );

        static string GetTitle(Error error) =>
            error.Type switch {
                ErrorType.VALIDATION => error.Code,
                ErrorType.PROBLEM => error.Code,
                ErrorType.NOT_FOUND => error.Code,
                ErrorType.CONFLICT => error.Code,
                _ => "Server failure"
            };

        static string GetDetail(Error error) =>
            error.Type switch {
                ErrorType.VALIDATION => error.Description,
                ErrorType.PROBLEM => error.Description,
                ErrorType.NOT_FOUND => error.Description,
                ErrorType.CONFLICT => error.Description,
                _ => "An unexpected error occurred"
            };

        static string GetType(ErrorType errorType) =>
            errorType switch {
                ErrorType.VALIDATION => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.PROBLEM => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NOT_FOUND => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.CONFLICT => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch {
                ErrorType.VALIDATION or ErrorType.PROBLEM => StatusCodes.Status400BadRequest,
                ErrorType.NOT_FOUND => StatusCodes.Status404NotFound,
                ErrorType.CONFLICT => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

        static Dictionary<string, object?>? GetErrors(Result result)
        {
            if (result.Error is not ValidationError validationError)
                return null;

            return new Dictionary<string, object?>
            {
                { "errors", validationError.Errors }
            };
        }
    }
}
