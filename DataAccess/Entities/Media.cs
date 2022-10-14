using DataAccess.Entities.Base;

namespace DataAccess.Entities;

/// <summary>
/// Media entity.
/// </summary>
public class Media : BaseIdEntity
{
    /// <summary>
    /// Gets or sets url.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets creation date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets group key.
    /// </summary>
    public Guid? GroupKey { get; set; }
}