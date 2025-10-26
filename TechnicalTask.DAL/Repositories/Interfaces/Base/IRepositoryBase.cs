using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using TechnicalTask.DAL.Repositories.QueryOptions;

namespace TechnicalTask.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryBase<T>
        where T : class
    {
        Task<T> CreateAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync(QueryOptions<T>? queryOptions = null);

        Task<T?> GetFirstOrDefaultAsync(QueryOptions<T>? queryOptions = null);
    }
}
