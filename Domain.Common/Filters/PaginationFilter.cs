namespace Domain.Common.Filters;

/// <summary>
/// Pagination filter.
/// </summary>
public class PaginationFilter
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

    /// <summary>
    /// Gets or sets page number.
    /// </summary>
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value > 50 ? 50 : value;
    }

    /// <summary>
    /// Gets or sets page size.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 1 : value;
    }
}