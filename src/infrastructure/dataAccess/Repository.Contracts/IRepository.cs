using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Repository.Contracts
{
    public interface IRepository
    {
        T Add<T>(T model) where T : class;
        void Update<T>(T model) where T : class;
        void Remove<T>(Expression<Func<T, bool>> filter) where T : class;
        Task SaveChanges();
        IQueryable<T> GetItems<T>(Expression<Func<T, bool>> filter = null) where T : class;
        Task<T> GetItem<T>(Expression<Func<T, bool>> filter) where T : class;

    }
}
