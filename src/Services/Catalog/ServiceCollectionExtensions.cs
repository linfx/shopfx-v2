using Catalog.Application.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static LinFxBuilder AddCatalog(this LinFxBuilder builder)
        {
            builder.Services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();
            return builder;
        }
    }
}
