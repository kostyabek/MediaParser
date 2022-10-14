using Domain.Common.Models.Base;

namespace Domain.Common.Models.Media;

/// <summary>
/// Media model.
/// </summary>
public class MediaModel : UrlModel
{
    /// <summary>
    /// Gets or sets creation date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets group key.
    /// </summary>
    public Guid? GroupKey { get; set; }
}