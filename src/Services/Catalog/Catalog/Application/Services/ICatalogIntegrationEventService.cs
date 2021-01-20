using LinFx.Extensions.EventBus.Abstractions;
using System.Threading.Tasks;

namespace Catalog.Application.Services
{
    public interface ICatalogIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IEvent evt);

        Task PublishThroughEventBusAsync(IEvent evt);
    }
}
