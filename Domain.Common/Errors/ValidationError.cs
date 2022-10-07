using FluentResults;

namespace Domain.Common.Errors;

/// <summary>
/// Validation error.
/// </summary>
public class ValidationError : Error
{
    /// <summary>
    /// Instantiates <see cref="ValidationError"/>.
    /// </summary>
    /// <param name="msg">A message.</param>
    public ValidationError(string msg) : base(msg)
    {
    }
}