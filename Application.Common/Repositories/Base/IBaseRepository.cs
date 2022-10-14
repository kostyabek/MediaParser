using DataAccess.Entities;
using Domain.Common.Filters;
using System.Linq.Expressions;

namespace Application.Common.Repositories.Base;

/// <summary>
/// Base repository.
/// </summary>
/// <typeparam name="T1">Target entity type.</typeparam>
/// <typeparam name="T2">Model entity type.</typeparam>
public interface IBaseRepository<T1, T2>
{
    /// <summary>
    /// Gets all records.
    /// </summary>
    /// <returns>All records.</returns>
    Task<(List<T2>, long)> GetAllAsync();

    /// <summary>
    /// Gets records with pagination.
    /// </summary>
    /// <param name="paginationFilter">Pagination filter.</param>
    /// <returns>A paginated set of records.</returns>
    Task<(List<T2>, long)> GetAllAsync(PaginationFilter paginationFilter);

    /// <summary>
    /// Finds records by provided criteria.
    /// </summary>
    /// <param name="filter">Criteria to filter records by.</param>
    /// <returns>A queried list of records.</returns>
    Task<List<Media>> FindAsync(Expression<Func<Media, bool>> filter);

    /// <summary>
    /// Finds records by provided criteria with pagination.
    /// </summary>
    /// <param name="filter">Criteria to filter records by.</param>
    /// <param name="paginationFilter">Pagination filter.</param>
    /// <returns>A paginated set of records.</returns>
    Task<List<Media>> FindAsync(Expression<Func<Media, bool>> filter, PaginationFilter paginationFilter);

    /// <summary>
    /// Gets record by its id.
    /// </summary>
    /// <param name="id">Id of the record.</param>
    /// <returns>Queried record.</returns>
    Task<T1> GetByIdAsync(string id);

    /// <summary>
    /// Creates a record.
    /// </summary>
    /// <param name="entity">Entity to map data from.</param>
    Task CreateAsync(T1 entity);

    /// <summary>
    /// Deletes a record by its id.
    /// </summary>
    /// <param name="id">Id of the record to delete.</param>
    /// <returns>Number of affected rows.</returns>
    Task<long> DeleteByIdAsync(string id);

    /// <summary>
    /// Deletes many records by their ids.
    /// </summary>
    /// <param name="ids">Ids of the records to delete.</param>
    /// <returns>Number of affected rows.</returns>
    Task<long> DeleteManyAsync(IEnumerable<string> ids);
}