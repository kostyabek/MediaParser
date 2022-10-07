using Domain.Common.Errors;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hosted.Common.Extensions;

/// <summary>
/// Contains extensions methods for <see cref="Result"/>.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts <see cref="Result"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="result"><see cref="Result"/> object to convert.</param>
    /// <returns>Correspondent <see cref="IActionResult"/>.</returns>
    public static IActionResult ToActionResult(this Result result)
    {
        return TryGetErrorActionResult(result, out var actionResult) ?
            actionResult :
            new OkObjectResult(new
            {
                messages = result.Reasons ?? new List<IReason>()
            });
    }

    /// <summary>
    /// Converts <see cref="Result{TValue}"/> to <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="result"><see cref="Result{TValue}"/> object to convert.</param>
    /// <returns>Correspondent <see cref="IActionResult"/>.</returns>
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        var nonGenericResult = result.ToResult();

        return TryGetErrorActionResult(nonGenericResult, out var actionResult) ?
            actionResult :
            new OkObjectResult(new
            {
                value = result.ValueOrDefault,
                messages = result.Reasons ?? new List<IReason>()
            });
    }

    private static bool TryGetErrorActionResult(ResultBase result, out IActionResult actionResult)
    {
        actionResult = default;

        if (result.IsSuccess)
        {
            return false;
        }

        var reasons = result.Reasons;

        if (result.HasError<NotFoundError>())
        {
            actionResult = new NotFoundObjectResult(reasons);
            return true;
        }

        if (result.HasError<InvalidOperationError>())
        {
            actionResult = new BadRequestObjectResult(reasons);
            return true;
        }

        if (result.HasError<UnauthorizedError>())
        {
            actionResult = new UnauthorizedObjectResult(reasons);
            return true;
        }

        actionResult = new ObjectResult(reasons) { StatusCode = StatusCodes.Status500InternalServerError };

        return true;
    }
}