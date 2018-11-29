using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Repository.Contracts
{
    public interface IReadOnlyRepository
    {
        IQueryable<T> GetItems<T>(Expression<Func<T, bool>> filter = null) where T : class;
        Task<T> GetItem<T>(Expression<Func<T, bool>> filter) where T : class;
    }
}
