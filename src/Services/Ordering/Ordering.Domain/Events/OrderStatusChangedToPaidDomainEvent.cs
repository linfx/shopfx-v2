using MediatR;
using Ordering.Domain.Models.OrderAggregate;
using System.Collections.Generic;

namespace Ordering.Domain.Events
{
    /// <summary>
    /// 订单支付事件
    /// </summary>
    public class OrderStatusChangedToPaidDomainEvent : INotification
    {
        public long OrderId { get; }

        public IEnumerable<OrderItem> OrderItems { get; }

        public OrderStatusChangedToPaidDomainEvent(long orderId, IEnumerable<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}