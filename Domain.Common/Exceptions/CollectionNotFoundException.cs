namespace Domain.Common.Exceptions;

/// <summary>
/// Collection not found excpeption.
/// </summary>
[Serializable]
public class CollectionNotFoundException : ApplicationException
{
    public CollectionNotFoundException(string message) : base(message) { }
}