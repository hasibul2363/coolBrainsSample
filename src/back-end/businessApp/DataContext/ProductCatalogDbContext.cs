using System;
using System.Collections.Generic;
using System.Text;
using DataContext.Configurations;
using Microsoft.EntityFrameworkCore;
using SuitSupply.Infrastructure.Repository;
using SuitSupply.ProductCatalog.DomainModels;

namespace DataContext
{
    public class ProductCatalogDbContext : BaseContext
    {
        public ProductCatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
