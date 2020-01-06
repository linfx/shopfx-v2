using MediatR;
using Ordering.Domain.Models.BuyerAggregate;

namespace Ordering.Domain.Events
{
    /// <summary>
    /// 客户支付方式验证-领域事件
    /// </summary>
    public class BuyerAndPaymentMethodVerifiedDomainEvent : INotification
    {
        /// <summary>
        /// 客户
        /// </summary>
        public Buyer Buyer { get; private set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public PaymentMethod Payment { get; private set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; private set; }

        public BuyerAndPaymentMethodVerifiedDomainEvent(Buyer buyer, PaymentMethod payment, long orderId)
        {
            Buyer = buyer;
            Payment = payment;
            OrderId = orderId;
        }
    }
}
