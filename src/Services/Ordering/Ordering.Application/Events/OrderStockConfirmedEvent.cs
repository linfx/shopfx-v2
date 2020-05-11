using LinFx.Extensions.EventBus;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderStockConfirmedEvent : Event
    {
        public long OrderId { get; }

        public OrderStockConfirmedEvent(long orderId) => OrderId = orderId;
    }
}