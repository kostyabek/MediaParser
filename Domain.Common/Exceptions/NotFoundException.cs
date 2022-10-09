namespace Domain.Common.Exceptions;

[Serializable]
public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message) { }
}