using System.Linq.Expressions;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace MyApi.Repository
{
    public class RepositoryBase<T, TContext> : IRepositoryBase<T, TContext>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<T> _dbSet;
        public RepositoryBase(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task RemoveAsync(T entity) => _dbSet.Remove(entity);
        public void UpdateAsync(T entity) => _dbSet.Update(entity);

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);
        public async Task<T> GetAsync(
            Expression<Func<T, bool>> predicate,
            string includeProperties = null,
            bool tracking = true
        )
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            if (!tracking)
                query = query.AsNoTracking();

            query = ApplyIncludes(query, includeProperties);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<(List<T> Items, int TotalCount)> GetAllAsync(
            int? pageNumber = null,
            int? pageSize = null,
            string includeProperties = null,
            string orderBy = null,
            bool ascending = true,
            bool tracking = true
        )
        {
            IQueryable<T> query = _dbSet;

            if (!tracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeProperties)) query = ApplyIncludes(query, includeProperties);

            if (!string.IsNullOrWhiteSpace(orderBy))
                query = ApplyOrdering(orderBy, ascending, query);

            int totalCount = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                int skip = (pageNumber.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            var items = await query.ToListAsync();

            return (items, totalCount);
        }

        private static IQueryable<T> ApplyOrdering(string orderBy, bool ascending, IQueryable<T> query)
        {
            var propertyInfo = typeof(T).GetProperty(orderBy,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var methodName = "OrderBy";

                if (!ascending)
                {
                    methodName = "OrderByDescending";
                }

                var resultExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    [typeof(T), propertyInfo.PropertyType],
                    query.Expression,
                    Expression.Quote(orderByExpression));

                query = query.Provider.CreateQuery<T>(resultExpression);
            }

            return query;
        }

        private IQueryable<T> ApplyIncludes(IQueryable<T> query, string includeProperties)
        {
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }
            return query;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
    }
}