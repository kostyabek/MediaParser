namespace Parser.Application.CQRS.Commands.Base;

public abstract class UrlCommand
{
    private readonly string _url = string.Empty;
    public string Url
    {
        get => _url;
        init => _url = value.EndsWith('/') ? value[..^1] : value;
    }
}