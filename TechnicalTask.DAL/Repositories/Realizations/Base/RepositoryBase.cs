using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TechnicalTask.DAL.Data;
using TechnicalTask.DAL.Repositories.Interfaces.Base;
using TechnicalTask.DAL.Repositories.QueryOptions;

namespace TechnicalTask.DAL.Repositories.Realizations.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly TechnicalTaskDbContext _dbContext;

        protected RepositoryBase(TechnicalTaskDbContext context)
        {
            _dbContext = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var tmp = await _dbContext.Set<T>().AddAsync(entity);
            return tmp.Entity;
        }
        public async Task<T?> GetFirstOrDefaultAsync(QueryOptions<T>? queryOptions = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (queryOptions != null)
            {
                query = ApplyTracking(query, queryOptions.AsNoTracking);
                query = ApplyInclude(query, queryOptions.Include);
                query = ApplyFilter(query, queryOptions.Filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(QueryOptions<T>? queryOptions = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (queryOptions != null)
            {
                query = ApplyQueryOptions(query, queryOptions);
            }

            return await query.ToListAsync();
        }

        private static IQueryable<T> ApplyFilter(IQueryable<T> query, Expression<Func<T, bool>>? filter)
        {
            return filter is not null ? query.Where(filter) : query;
        }

        private static IQueryable<T> ApplyInclude(IQueryable<T> query, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include)
        {
            return include is not null ? include(query) : query;
        }

        private static IQueryable<T> ApplyOrdering(
            IQueryable<T> query,
            Expression<Func<T, object>>? orderByASC,
            Expression<Func<T, object>>? orderByDESC)
        {
            if (orderByASC != null)
            {
                return query.OrderBy(orderByASC);
            }

            if (orderByDESC != null)
            {
                return query.OrderByDescending(orderByDESC);
            }

            return query;
        }

        private static IQueryable<T> ApplyPagination(IQueryable<T> query, int offset, int limit)
        {
            if (offset > 0)
            {
                query = query.Skip(offset);
            }

            if (limit > 0)
            {
                query = query.Take(limit);
            }

            return query;
        }

        static private IQueryable<T> ApplyTracking(IQueryable<T> query, bool asNoTracking)
        {
            return asNoTracking ? query.AsNoTracking() : query;
        }

        private static IQueryable<T> ApplyQueryOptions(IQueryable<T> query, QueryOptions<T> queryOptions)
        {
            query = ApplyTracking(query, queryOptions.AsNoTracking);
            query = ApplyInclude(query, queryOptions.Include);
            query = ApplyFilter(query, queryOptions.Filter);
            query = ApplyOrdering(query, queryOptions.OrderByASC, queryOptions.OrderByDESC);
            query = ApplyPagination(query, queryOptions.Offset, queryOptions.Limit);

            return query;
        }
    }
}
