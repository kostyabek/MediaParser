using HtmlAgilityPack;
using Parser.Application.Services.Parsing;

namespace Parser.Infrastructure.Services.Parsing;

/// <inheritdoc cref="IJoyReactorParsingService"/>
public class JoyReactorParsingService : IJoyReactorParsingService
{
    /// <inheritdoc/>
    public IEnumerable<string> ParsePost(HtmlNode postNode)
    {
        var imagesUrls = postNode
            .Descendants("div")
            .Where(e => e.HasClass("post_content"))
            .SelectMany(e => e.Descendants("div"))
            .Where(e => e.HasClass("image"))
            .SelectMany(e => e.Descendants("img"))
            .Select(e => e.GetAttributeValue("src", "*"))
            .ToList();

        return imagesUrls;
    }

    /// <inheritdoc/>
    public (IEnumerable<HtmlNode>, string) ParsePage(string html)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var postNodes = htmlDoc.DocumentNode
            .Descendants("div")
            .Where(e => e.HasClass("postContainer"))
            .ToList();

        var nextPageNode = htmlDoc.DocumentNode
            .Descendants("div")
            .Where(e => e.Id == "Pagination")
            .SelectMany(e => e.Descendants("a").Where(a => a.HasClass("next")))
            .SingleOrDefault();

        var nextPageRelativeUrl = nextPageNode == null ?
            string.Empty :
            nextPageNode.GetAttributeValue("href", string.Empty);

        return (postNodes, nextPageRelativeUrl);
    }
}