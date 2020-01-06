using MediatR;
using Ordering.Domain.Models.OrderAggregate;

namespace Ordering.Domain.Events
{
    /// <summary>
    /// 订单发货 - 领域事件
    /// </summary>
    public class OrderShippedDomainEvent : INotification
    {
        /// <summary>
        /// 订单
        /// </summary>
        public Order Order { get; }

        public OrderShippedDomainEvent(Order order)
        {
            Order = order;           
        }
    }
}
