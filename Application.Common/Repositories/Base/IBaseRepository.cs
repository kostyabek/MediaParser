using Domain.Common.Filters;

namespace Application.Common.Repositories.Base;

public interface IBaseRepository<T>
{
    Task<List<T>> GetAllWithPaginationAsync(PaginationFilter paginationFilter);
    Task<T> GetByIdAsync(string id);
    Task CreateAsync(T entity);
    Task<long> DeleteAsync(string id);
}