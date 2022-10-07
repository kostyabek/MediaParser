using FluentResults;

namespace Domain.Common.Errors;

/// <summary>
/// Not found error.
/// </summary>
public class NotFoundError : Error
{
    /// <summary>
    /// Instantiates <see cref="NotFoundError"/>.
    /// </summary>
    /// <param name="msg">A message.</param>
    public NotFoundError(string msg) : base(msg)
    {
    }
}