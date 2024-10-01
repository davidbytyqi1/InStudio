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
    }
}
