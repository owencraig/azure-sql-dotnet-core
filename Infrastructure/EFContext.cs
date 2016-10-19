using Microsoft.EntityFrameworkCore;
using AzureSqlDotnetCore.Models;
namespace AzureSqlDotnetCore.Infrastructure
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        { 
            this.Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }

    }
}