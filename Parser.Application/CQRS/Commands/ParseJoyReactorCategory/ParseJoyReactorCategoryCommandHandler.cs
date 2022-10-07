using Application.Common.Repositories;
using DataAccess.Entities;
using FluentResults;
using MediatR;
using NLog;
using Parser.Application.HttpClients;
using Parser.Application.Services.Parsing;
using Parser.Domain;

namespace Parser.Application.CQRS.Commands.ParseJoyReactorCategory;

public class ParseJoyReactorCategoryCommandHandler : IRequestHandler<ParseJoyReactorCategoryCommand, Result>
{
    private readonly IJoyReactorParsingService _parsingService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IMediaRepository _mediaRepository;
    private readonly IHtmlHttpClient _httpClient;

    public ParseJoyReactorCategoryCommandHandler(
        IJoyReactorParsingService parsingService,
        IMediaRepository mediaRepository,
        IHtmlHttpClient httpClient)
    {
        _parsingService = parsingService;
        _mediaRepository = mediaRepository;
        _httpClient = httpClient;
    }

    public async Task<Result> Handle(ParseJoyReactorCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var operationTimestamp = DateTime.UtcNow;

            var url = request.Url;
            var currentPageNumberString = "1000000000";
            string previousPageNumberString;
            
            var tagNameIndex = url.IndexOf("tag/", StringComparison.Ordinal);
            if (tagNameIndex == -1)
            {
                return Result.Fail("Could not extract tag name from given URL.");
            }

            var lastIndexOfSlash = url.LastIndexOf('/');
            var tagName = tagNameIndex + 3 == lastIndexOfSlash
                ? url[(tagNameIndex + 4)..]
                : url.Substring(tagNameIndex + 4, lastIndexOfSlash - tagNameIndex - 4);

            do
            {
                previousPageNumberString = currentPageNumberString;

                var html = await _httpClient.GetHtml(url);
                var (postNodes, nextPageRelativeUrl) = _parsingService.ParsePage(html);

                if (nextPageRelativeUrl.Length == 0)
                {
                    url = string.Empty;
                }
                else
                {
                    var pageNumberIndex = nextPageRelativeUrl.LastIndexOf('/');
                    currentPageNumberString = nextPageRelativeUrl[(pageNumberIndex + 1)..];
                    url = $"{AppConsts.BaseSiteUrls.JoyReactor.BaseDomainName}/tag/{tagName}/{currentPageNumberString}";
                }

                foreach (var postNode in postNodes)
                {
                    var imagesUrls = _parsingService.ParsePost(postNode);

                    Guid? groupKey = Guid.NewGuid();
                    foreach (var imageUrl in imagesUrls)
                    {
                        if (imagesUrls.Count() == 1)
                        {
                            groupKey = null;
                        }

                        var media = new Media
                        {
                            Url = imageUrl.Trim('/', '\\'),
                            CreatedDate = operationTimestamp,
                            GroupKey = groupKey
                        };

                        await _mediaRepository.CreateAsync(media);
                    }
                }
            } while (url != string.Empty && Convert.ToInt32(currentPageNumberString) < Convert.ToInt32(previousPageNumberString));

            return Result.Ok();
        }
        catch (Exception e)
        {
            const string message = $"Error while executing {nameof(ParseJoyReactorCategoryCommandHandler)}.";
            _logger.Error(e, message);
            return Result.Fail(message);
        }
    }
}