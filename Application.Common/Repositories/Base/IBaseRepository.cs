using DataAccess.Entities;
using Domain.Common.Filters;
using System.Linq.Expressions;

namespace Application.Common.Repositories.Base;

public interface IBaseRepository<T1, T2>
{
    Task<(List<T2>, long)> GetAllAsync();
    Task<(List<T2>, long)> GetAllAsync(PaginationFilter paginationFilter);
    Task<List<Media>> FindAsync(Expression<Func<Media, bool>> filter);
    Task<List<Media>> FindAsync(Expression<Func<Media, bool>> filter, PaginationFilter paginationFilter);
    Task<T1> GetByIdAsync(string id);
    Task CreateAsync(T1 entity);
    Task<long> DeleteByIdAsync(string id);
    Task<long> DeleteManyAsync(IEnumerable<string> ids);
}