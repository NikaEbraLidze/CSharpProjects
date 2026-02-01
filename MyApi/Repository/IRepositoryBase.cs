using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace MyApi.Repository
{
    public interface IRepositoryBase<T, TContext>
        where T : class
        where TContext : DbContext
    {
        public Task AddAsync(T entity);
        public Task RemoveAsync(T entity);
        public void Update(T entity);
        public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        public Task<T> GetAsync(
            Expression<Func<T, bool>> predicate,
            string includeProperties = null,
            bool tracking = true
        );
        public Task<(List<T> Items, int TotalCount)> GetAllAsync(
            int? pageNumber,
            int? pageSize,
            string includeProperties = null,
            string orderBy = null,
            bool ascending = true,
            bool tracking = true
        );
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}