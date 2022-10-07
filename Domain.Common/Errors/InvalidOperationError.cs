using FluentResults;

namespace Domain.Common.Errors;

/// <summary>
/// Invalid operation error.
/// </summary>
public class InvalidOperationError : Error
{
    /// <summary>
    /// Instantiates <see cref="InvalidOperationError"/>.
    /// </summary>
    /// <param name="msg">A message.</param>
    public InvalidOperationError(string msg) : base(msg)
    {
    }
}