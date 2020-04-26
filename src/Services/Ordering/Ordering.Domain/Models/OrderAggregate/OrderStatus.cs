using LinFx.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.Models.OrderAggregate
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public class OrderStatus : Enumeration
    {
        /// <summary>
        /// 订单提交
        /// </summary>
        public static OrderStatus Submitted = new OrderStatus(1, nameof(Submitted).ToLowerInvariant());

        /// <summary>
        /// 等待确认
        /// </summary>
        public static OrderStatus AwaitingValidation = new OrderStatus(2, nameof(AwaitingValidation).ToLowerInvariant());

        /// <summary>
        /// 库存确认
        /// </summary>
        public static OrderStatus StockConfirmed = new OrderStatus(3, nameof(StockConfirmed).ToLowerInvariant());

        /// <summary>
        /// 已支付
        /// </summary>
        public static OrderStatus Paid = new OrderStatus(4, nameof(Paid).ToLowerInvariant());

        /// <summary>
        /// 已发货
        /// </summary>
        public static OrderStatus Shipped = new OrderStatus(5, nameof(Shipped).ToLowerInvariant());

        /// <summary>
        /// 取消
        /// </summary>
        public static OrderStatus Cancelled = new OrderStatus(6, nameof(Cancelled).ToLowerInvariant());

        protected OrderStatus() { }

        public OrderStatus(int id, string name) : base(id, name) { }

        /// <summary>
        /// 订单状举列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<OrderStatus> List() => new[] { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled };

        public static OrderStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new OrderingDomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static OrderStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new OrderingDomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}