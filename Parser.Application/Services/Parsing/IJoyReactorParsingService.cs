using HtmlAgilityPack;

namespace Parser.Application.Services.Parsing;

/// <summary>
/// Joyreactor parsing service.
/// </summary>
public interface IJoyReactorParsingService
{
    /// <summary>
    /// Parses joyreactor page.
    /// </summary>
    /// <param name="postNode">A node with a post.</param>
    /// <returns>Collection of extracted URLs to posts.</returns>
    IEnumerable<string> ParsePost(HtmlNode postNode);

    /// <summary>
    /// Parses joyreactor post.
    /// </summary>
    /// <param name="html">Raw HTML.</param>
    /// <returns>Collection of extracted nodes of posts and a url to the next page.</returns>
    (IEnumerable<HtmlNode>, string) ParsePage(string html);
}