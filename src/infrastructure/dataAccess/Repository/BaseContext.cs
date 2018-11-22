using Microsoft.EntityFrameworkCore;

namespace SuitSupply.Infrastructure.Repository
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options):base(options)
        {
        }
       
    }
}
