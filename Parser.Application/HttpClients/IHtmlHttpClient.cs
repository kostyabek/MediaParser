namespace Parser.Application.HttpClients;

/// <summary>
/// Media HTTP-client.
/// </summary>
public interface IHtmlHttpClient
{
    /// <summary>
    /// Retrieves HTML of a page by its URL.
    /// </summary>
    /// <param name="url">URL of a target page.</param>
    /// <returns>Raw HTML.</returns>
    Task<string?> GetHtml(string url);
}