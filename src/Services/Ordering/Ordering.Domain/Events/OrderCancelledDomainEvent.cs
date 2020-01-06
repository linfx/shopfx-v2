using MediatR;
using Ordering.Domain.Models.OrderAggregate;

namespace Ordering.Domain.Events
{
    /// <summary>
    /// 订单取消_领域事件
    /// </summary>
    public class OrderCancelledDomainEvent : INotification
    {
        /// <summary>
        /// 订单
        /// </summary>
        public Order Order { get; }

        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
