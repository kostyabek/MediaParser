using Domain.Common.Filters;

namespace Application.Common.Repositories.Base;

public interface IBaseRepository<T1, T2>
{
    Task<(List<T2>, long)> GetAllWithPaginationAsync(PaginationFilter paginationFilter);
    Task<T1> GetByIdAsync(string id);
    Task CreateAsync(T1 entity);
    Task<long> DeleteAsync(string id);
}