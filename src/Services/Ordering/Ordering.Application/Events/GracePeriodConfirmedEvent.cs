using LinFx.Extensions.EventBus;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class GracePeriodConfirmedEvent : Event
    {
        public long OrderId { get; }

        public GracePeriodConfirmedEvent(long orderId) => OrderId = orderId;
    }
}
