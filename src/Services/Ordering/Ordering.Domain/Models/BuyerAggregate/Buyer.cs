using LinFx.Domain.Models;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.Models.BuyerAggregate
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Buyer : AggregateRoot<long>
    {
        private readonly List<PaymentMethod> _paymentMethods;

        protected Buyer()
        {
            _paymentMethods = new List<PaymentMethod>();
        }

        public Buyer(long id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

        /// <summary>
        /// 验证支付方式
        /// </summary>
        /// <param name="cardTypeId"></param>
        /// <param name="alias"></param>
        /// <param name="cardNumber"></param>
        /// <param name="securityNumber"></param>
        /// <param name="cardHolderName"></param>
        /// <param name="expiration"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public PaymentMethod VerifyOrAddPaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration, long orderId)
        {
            var existingPayment = _paymentMethods.SingleOrDefault(p => p.IsEqualTo(cardTypeId, cardNumber, expiration));
            if (existingPayment != null)
            {
                AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, existingPayment, orderId));
                return existingPayment;
            }
            else
            {
                var payment = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration);
                _paymentMethods.Add(payment);
                AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, payment, orderId));
                return payment;
            }
        }
    }
}
