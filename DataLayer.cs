using Microsoft.EntityFrameworkCore;
public class EFContext : DbContext
{
    public EFContext(DbContextOptions<EFContext> options): base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductSpecification>().Property(p => p.Id).ValueGeneratedNever();
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSpecification> ProductSpecifications { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductSubCategory> ProductSubCategories { get; set; }

}