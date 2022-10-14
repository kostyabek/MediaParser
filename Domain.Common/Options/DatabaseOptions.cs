namespace Domain.Common.Options;

/// <summary>
/// Database options.
/// </summary>
public class DatabaseOptions
{
    /// <summary>
    /// Gets or sets connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets database name.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// Gets or sets media collection name.
    /// </summary>
    public string MediaCollectionName { get; set; }
}