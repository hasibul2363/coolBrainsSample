using Microsoft.EntityFrameworkCore;
using Repository.Test.Models;
using SuitSupply.Infrastructure.Repository;

namespace Repository.Test
{
    public class TestDbContext : BaseContext
    {

        public TestDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
