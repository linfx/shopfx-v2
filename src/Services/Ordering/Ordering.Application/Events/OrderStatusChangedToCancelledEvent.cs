namespace Ordering.API.Application.IntegrationEvents.Events
{
    using LinFx.Extensions.EventBus;

    public class OrderStatusChangedToCancelledEvent : Event
    {
        public long OrderId { get; }

        public string OrderStatus { get; }

        public string BuyerName { get; }

        public OrderStatusChangedToCancelledEvent(long orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }
}