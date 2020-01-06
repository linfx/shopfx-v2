using MediatR;

namespace Ordering.Domain.Events
{
    /// <summary>
    /// Event used when the order stock items are confirmed
    /// </summary>
    public class OrderStatusChangedToStockConfirmedDomainEvent : INotification
    {
        public long OrderId { get; }

        public OrderStatusChangedToStockConfirmedDomainEvent(long orderId) => OrderId = orderId;
    }
}