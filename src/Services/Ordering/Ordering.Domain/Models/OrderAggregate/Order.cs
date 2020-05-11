using LinFx.Domain.Models;
using Ordering.Domain.Events;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.Models.OrderAggregate
{
    /// <summary>
    /// 订单
    /// </summary>
    public partial class Order : AggregateRoot<long>
    {
        private long? _buyerId;
        private int? _paymentMethodId;
        private string _description;
        private DateTime _orderDate;
        private bool _isDraft;

        public long? GetBuyerId => _buyerId;

        /// <summary>
        /// 订单地址
        /// </summary>
        public Address Address { get; private set; }

        public Order(long userId, string userName, Address address, int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration, int? buyerId = null, int? paymentMethodId = null) : this()
        {
            _buyerId = buyerId;
            _paymentMethodId = paymentMethodId;
            _orderStatusId = OrderStatus.Submitted.Id;
            _orderDate = DateTime.UtcNow;
            Address = address;

            AddOrderStartedDomainEvent(userId, userName, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
        }

        /// <summary>
        /// 订单预览
        /// </summary>
        /// <returns></returns>
        public static Order NewDraft()
        {
            return new Order
            {
                _isDraft = true
            };
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <param name="id"></param>
        public void SetPaymentId(int id) => _paymentMethodId = id;

        /// <summary>
        /// 客户
        /// </summary>
        /// <param name="id"></param>
        public void SetBuyerId(long id) => _buyerId = id;

        /// <summary>
        /// 新建订单事件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="cardTypeId"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cardSecurityNumber"></param>
        /// <param name="cardHolderName"></param>
        /// <param name="cardExpiration"></param>
        private void AddOrderStartedDomainEvent(long userId, string userName, int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            AddDomainEvent(new OrderStartedDomainEvent(this, userId, userName, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration));
        }
    }

    /// <summary>
    /// 订单明细
    /// </summary>
    public partial class Order
    {
        private readonly List<OrderItem> _orderItems;

        /// <summary>
        /// 订单明细
        /// </summary>
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        /// <summary>
        /// 获取总金额
        /// </summary>
        /// <returns></returns>
        public decimal GetTotal() => _orderItems.Sum(o => o.GetUnits() * o.GetUnitPrice());

        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="productId">商品Id</param>
        /// <param name="productName">商品名称</param>
        /// <param name="unitPrice">单价</param>
        /// <param name="discount">折扣</param>
        /// <param name="pictureUrl">商品图片</param>
        /// <param name="units">数量</param>
        public void AddOrderItem(long productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            var existingOrderForProduct = _orderItems.SingleOrDefault(o => o.ProductId == productId);
            if (existingOrderForProduct != null)
            {
                if (discount > existingOrderForProduct.GetCurrentDiscount())
                    existingOrderForProduct.SetNewDiscount(discount);

                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
                _orderItems.Add(orderItem);
            }
        }
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public partial class Order
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        private int _orderStatusId;

        /// <summary>
        /// 订单状态机
        /// </summary>
        private readonly StateMachine<int, Trigger> _machine;

        /// <summary>
        /// 订单操作
        /// </summary>
        private enum Trigger { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled }

        /// <summary>
        /// 订单
        /// </summary>
        protected Order()
        {
            _isDraft = false;
            _orderItems = new List<OrderItem>();

            // 初始化状态机
            _machine = new StateMachine<int, Trigger>(()=> _orderStatusId, s => SetState(s));

            // 状态机流程配置
            _machine.Configure(OrderStatus.Submitted.Id)
                .Permit(Trigger.AwaitingValidation, OrderStatus.AwaitingValidation.Id); // 提交 -> 等待确认 

            _machine.Configure(OrderStatus.AwaitingValidation.Id)
                .Permit(Trigger.StockConfirmed, OrderStatus.StockConfirmed.Id);        // 等待确认 -> 库存确认 
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus OrderStatus { get; private set; }

        /// <summary>
        /// 等待确认
        /// </summary>
        public void SetAwaitingValidationStatus()
        {
            if (_orderStatusId == OrderStatus.Submitted.Id)
            {
                AddDomainEvent(new OrderStatusChangedToAwaitingValidationDomainEvent(Id, _orderItems));
                _machine.Fire(Trigger.AwaitingValidation);
            }
        }

        /// <summary>
        /// 库存确认
        /// </summary>
        public void SetStockConfirmedStatus()
        {
            if (_orderStatusId == OrderStatus.AwaitingValidation.Id)
            {
                AddDomainEvent(new OrderStatusChangedToStockConfirmedDomainEvent(Id));
                _machine.Fire(Trigger.StockConfirmed);
                _description = "All the items were confirmed with available stock.";
            }
        }

        /// <summary>
        /// 订单支付
        /// </summary>
        public void SetPaidStatus()
        {
            if (_orderStatusId == OrderStatus.StockConfirmed.Id)
            {
                AddDomainEvent(new OrderStatusChangedToPaidDomainEvent(Id, OrderItems));
                _machine.Fire(Trigger.Paid);
                _description = "The payment was performed at a simulated \"American Bank checking bank account endinf on XX35071\"";
            }
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        public void SetShippedStatus()
        {
            if (_orderStatusId != OrderStatus.Paid.Id)
                StatusChangeException(OrderStatus.Shipped);

            _machine.Fire(Trigger.Shipped);
            _description = "The order was shipped.";
            AddDomainEvent(new OrderShippedDomainEvent(this));
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        public void SetCancelledStatus()
        {
            if (_orderStatusId == OrderStatus.Paid.Id || _orderStatusId == OrderStatus.Shipped.Id)
                StatusChangeException(OrderStatus.Cancelled);

            _machine.Fire(Trigger.Cancelled);
            _description = $"The order was cancelled.";
            AddDomainEvent(new OrderCancelledDomainEvent(this));
        }

        /// <summary>
        /// 取消订单 - 库存不足
        /// </summary>
        /// <param name="orderStockRejectedItems"></param>
        public void SetCancelledStatusWhenStockIsRejected(IEnumerable<long> orderStockRejectedItems)
        {
            if (_orderStatusId == OrderStatus.AwaitingValidation.Id)
            {
                _machine.Fire(Trigger.Cancelled);

                var itemsStockRejectedProductNames = OrderItems
                    .Where(c => orderStockRejectedItems.Contains(c.ProductId))
                    .Select(c => c.GetOrderItemProductName());

                var itemsStockRejectedDescription = string.Join(", ", itemsStockRejectedProductNames);
                _description = $"The product items don't have stock: ({itemsStockRejectedDescription}).";
            }
        }

        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="orderStatusId"></param>
        private void SetState(int orderStatusId)
        {
            _orderStatusId = orderStatusId;
        }

        private void StatusChangeException(OrderStatus orderStatusToChange) => throw new OrderingDomainException($"Is not possible to change the order status from {OrderStatus.Name} to {orderStatusToChange.Name}.");
    }
}