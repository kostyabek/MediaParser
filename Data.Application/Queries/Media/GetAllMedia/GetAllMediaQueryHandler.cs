using Application.Common.Repositories;
using FluentResults;
using MediatR;
using NLog;

namespace Data.Application.Queries.Media.GetAllMedia;

/// <summary>
/// Handles <see cref="GetAllMediaQuery"/>.
/// </summary>
public class GetAllMediaQueryHandler : IRequestHandler<GetAllMediaQuery, Result<GetAllMediaQueryResult>>
{
    private readonly IMediaRepository _mediaRepository;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Instantiates <see cref="GetAllMediaQueryHandler"/>.
    /// </summary>
    /// <param name="mediaRepository">Media repository.</param>
    public GetAllMediaQueryHandler(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<GetAllMediaQueryResult>> Handle(GetAllMediaQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var (models, totalCount) = await _mediaRepository
                .GetAllAsync(request.PaginationFilter);

            var result = new GetAllMediaQueryResult
            {
                Models = models,
                TotalCount = totalCount
            };

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            const string message = $"Error while executing {nameof(GetAllMediaQueryHandler)}.";
            _logger.Error(e, message);
            return Result.Fail(new [] { message });
        }
    }
}