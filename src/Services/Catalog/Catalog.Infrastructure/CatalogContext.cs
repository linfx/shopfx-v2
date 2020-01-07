using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Models;
using Catalog.EntityFrameworkCore.EntityConfigurations;

namespace Catalog.EntityFrameworkCore
{
    public class CatalogContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }

        public DbSet<CatalogBrand> CatalogBrands { get; set; }

        public DbSet<CatalogType> CatalogTypes { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                //.UseMySql("server=10.10.5.107;database=catalogdb;uid=root;pwd=root;")
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=catalogdb;Trusted_Connection=True;MultipleActiveResultSets=true");
                
            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
