using InStudio.Common;
using InStudio.Common.Types;
using System.Linq.Expressions;

namespace InStudio.Services.Repositories.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task AddAsync(T entity);

        void Update(T entity);

        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize);

        Task DeleteAsync(T entity);

        Task SaveChangesAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> criteria);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<PagedReadOnlyCollection<TResult>> GetPagedWithFilterAndProjectToAsync<TResult>(Expression<Func<T, bool>> criteria, PageableParams pagingParams, SortParameter sortParamters)
            where TResult : class;

        Task<PagedReadOnlyCollection<TResult>> GetPagedWithFilterAndProjectToAsync<TResult>(Expression<Func<T, bool>> criteria, PageableParams pagingParams, SortParameter sortParameters, params string[] includes) where TResult : class;

        Task<T> GetSingleByCriteriaAsync(Expression<Func<T, bool>> criteria, params string[] includes);

        Task<List<T>> ListByCriteriaAsync(Expression<Func<T, bool>> criteria, params string[] includes);

        Task<int> CountByCriteriaAsync(Expression<Func<T, bool>> criteria);
    }
}
