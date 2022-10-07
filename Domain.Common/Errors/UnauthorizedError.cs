using FluentResults;

namespace Domain.Common.Errors;

/// <summary>
/// Unauthorized error.
/// </summary>
public class UnauthorizedError : Error
{
    /// <summary>
    /// Instantiates <see cref="UnauthorizedError"/>.
    /// </summary>
    /// <param name="msg">A message.</param>
    public UnauthorizedError(string msg) : base(msg)
    {
    }
}