using LinFx.Extensions.EventBus;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderPaymentSuccededEvent : Event
    {
        public long OrderId { get; }

        public OrderPaymentSuccededEvent(long orderId) => OrderId = orderId;
    }

    public class OrderPaymentFailedEvent : Event
    {
        public long OrderId { get; }

        public OrderPaymentFailedEvent(long orderId) => OrderId = orderId;
    }
}