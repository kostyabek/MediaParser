using Domain.Common.Models.Media;

namespace Data.Application.Queries.Media.GetAllMedia;

/// <summary>
/// Represents a result for <see cref="GetAllMediaQuery"/>.
/// </summary>
public class GetAllMediaQueryResult
{
    /// <summary>
    /// Gets or sets resulting media models collection.
    /// </summary>
    public IEnumerable<MediaModel> Models { get; set; }

    /// <summary>
    /// Gets or sets a total count of models.
    /// </summary>
    public long TotalCount { get; set; }
}