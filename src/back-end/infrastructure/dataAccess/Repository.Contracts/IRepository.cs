using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Repository.Contracts
{
    public interface IRepository : IReadOnlyRepository
    {
        T Add<T>(T model) where T : class;
        void Update<T>(T model) where T : class;
        void Remove<T>(Expression<Func<T, bool>> filter) where T : class;
        Task SaveChanges();
       

    }
}
