namespace Domain.Common.Exceptions;

/// <summary>
/// Not found exception.
/// </summary>
[Serializable]
public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message) { }
}