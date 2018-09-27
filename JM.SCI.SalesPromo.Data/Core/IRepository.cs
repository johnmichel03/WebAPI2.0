using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JM.SCI.SalesPromo.Data.Core
{
    public interface IRepository
    {
        IQueryable<T> Query<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : class;
        T Add<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        void Save();
        Task SaveAsync();
    }
}

