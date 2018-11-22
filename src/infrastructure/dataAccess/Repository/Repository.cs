using Microsoft.EntityFrameworkCore;
using SuitSupply.Infrastructure.Repository.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuitSupply.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private readonly BaseContext _dbContext;
        public Repository(BaseContext context) => _dbContext = context;
        public Task<T> GetItem<T>(Expression<Func<T, bool>> filter) where T : class => _dbContext.Set<T>().FirstOrDefaultAsync(filter);

        public IQueryable<T> GetItems<T>(Expression<Func<T, bool>> filter = null) where T : class
        {
            return filter == null ? _dbContext.Set<T>() : _dbContext.Set<T>().Where(filter);
        }

        public T Add<T>(T model) where T : class => _dbContext.Set<T>().Add(model).Entity;

        public void Remove<T>(Expression<Func<T, bool>> filter) where T : class
        {
            var entitySet = _dbContext.Set<T>();
            entitySet.RemoveRange(entitySet.Where(filter));
        }

        public void Update<T>(T model) where T : class
        {
            _dbContext.Set<T>().Attach(model);
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public Task SaveChanges() => _dbContext.SaveChangesAsync();

    }
}
