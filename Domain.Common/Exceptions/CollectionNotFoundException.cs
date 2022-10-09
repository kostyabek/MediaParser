namespace Domain.Common.Exceptions;

[Serializable]
public class CollectionNotFoundException : ApplicationException
{
    public CollectionNotFoundException(string message) : base(message) { }
}