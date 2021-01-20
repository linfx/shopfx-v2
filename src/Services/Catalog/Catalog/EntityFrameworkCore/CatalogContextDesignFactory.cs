using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Catalog.EntityFrameworkCore
{
    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                .UseNpgsql("server=10.0.1.222;database=catalog;username=postgres;password=123456;");

            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
