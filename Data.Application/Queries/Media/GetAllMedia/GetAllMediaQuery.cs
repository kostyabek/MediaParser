using Domain.Common.Filters;
using FluentResults;
using MediatR;

namespace Data.Application.Queries.Media.GetAllMedia;

/// <summary>
/// Requests all existing media with pagination.
/// </summary>
public class GetAllMediaQuery : IRequest<Result<GetAllMediaQueryResult>>
{
    /// <summary>
    /// Gets or sets pagination filter.
    /// </summary>
    public PaginationFilter PaginationFilter { get; init; }
}