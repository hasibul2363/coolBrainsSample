using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SuitSupply.Infrastructure.Repository;
using SuitSupply.Infrastructure.Repository.Contracts;

namespace Repository.Test
{
    public abstract class TestBase
    {
        public Container Container { get; set; }
        public void InitContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<BaseContext>(()=> {
                var builder = new DbContextOptionsBuilder();
                builder.UseInMemoryDatabase(databaseName: "Add_writes_to_database");
                return new TestDbContext(builder.Options);
            }, Lifestyle.Scoped);
            container.Register<IRepository, SuitSupply.Infrastructure.Repository.Repository>();
            container.Verify();
            Container = container;
        }

    }
}


