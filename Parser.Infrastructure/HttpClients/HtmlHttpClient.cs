using Parser.Application.HttpClients;
using RestSharp;

namespace Parser.Infrastructure.HttpClients;

/// <inheritdoc cref="IHtmlHttpClient"/>
public class HtmlHttpClient : IHtmlHttpClient
{
    private readonly RestClient _httpClient;

    /// <summary>
    /// Instantiates <see cref="HtmlHttpClient"/>.
    /// </summary>
    /// <param name="httpClient">Base HTTP-client.</param>
    public HtmlHttpClient(RestClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<string?> GetHtml(string url)
    {
        var request = new RestRequest(new Uri(url));
        var response = await _httpClient.ExecuteAsync(request);
        return response.Content;
    }
}