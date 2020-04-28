using LinFx.Extensions.EventBus;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderPaymentSuccededIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; }

        public OrderPaymentSuccededIntegrationEvent(long orderId) => OrderId = orderId;
    }

    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; }

        public OrderPaymentFailedIntegrationEvent(long orderId) => OrderId = orderId;
    }
}