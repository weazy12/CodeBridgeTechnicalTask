using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace TechnicalTask.DAL.Repositories.QueryOptions
{
    public record QueryOptions<T>
    {
        public Expression<Func<T, bool>>? Filter { get; set; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>>? Include { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Expression<Func<T, object>>? OrderByASC { get; set; }
        public Expression<Func<T, object>>? OrderByDESC { get; set; }
        public bool AsNoTracking { get; set; } = true;
    }
}
