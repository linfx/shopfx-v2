using LinFx.Domain.Models;
using Ordering.Domain.Events;
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
        /// Value Object pattern example persisted as EF Core 2.0 owned entity
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        protected Order()
        {
            _isDraft = false;
            _orderItems = new List<OrderItem>();
        }

        public Order(long userId, string userName, Address address, int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration, int? buyerId = null, int? paymentMethodId = null) : this()
        {
            _buyerId = buyerId;
            _paymentMethodId = paymentMethodId;
            _orderStatusId = OrderStatus.Submitted.Id;
            _orderDate = DateTime.UtcNow;
            Address = address;

            // Add the OrderStarterDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddOrderStartedDomainEvent(userId, userName, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
        }

        /// <summary>
        /// 订单预览
        /// </summary>
        /// <returns></returns>
        public static Order NewDraft()
        {
            var order = new Order
            {
                _isDraft = true
            };
            return order;
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
        /// 获取总金额
        /// </summary>
        /// <returns></returns>
        public decimal GetTotal() => _orderItems.Sum(o => o.GetUnits() * o.GetUnitPrice());

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
            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
            AddDomainEvent(orderStartedDomainEvent);
        }
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public partial class Order
    {
        private int _orderStatusId;

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
                _orderStatusId = OrderStatus.AwaitingValidation.Id;
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

                _orderStatusId = OrderStatus.StockConfirmed.Id;
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

                _orderStatusId = OrderStatus.Paid.Id;
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

            _orderStatusId = OrderStatus.Shipped.Id;
            _description = "The order was shipped.";
            AddDomainEvent(new OrderShippedDomainEvent(this));
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        public void SetCancelledStatus()
        {
            if (_orderStatusId == OrderStatus.Paid.Id ||
                _orderStatusId == OrderStatus.Shipped.Id)
            {
                StatusChangeException(OrderStatus.Cancelled);
            }

            _orderStatusId = OrderStatus.Cancelled.Id;
            _description = $"The order was cancelled.";
            AddDomainEvent(new OrderCancelledDomainEvent(this));
        }

        /// <summary>
        /// 取消订单 - 库存不足
        /// </summary>
        /// <param name="orderStockRejectedItems"></param>
        public void SetCancelledStatusWhenStockIsRejected(IEnumerable<int> orderStockRejectedItems)
        {
            if (_orderStatusId == OrderStatus.AwaitingValidation.Id)
            {
                _orderStatusId = OrderStatus.Cancelled.Id;

                var itemsStockRejectedProductNames = OrderItems
                    .Where(c => orderStockRejectedItems.Contains(c.ProductId))
                    .Select(c => c.GetOrderItemProductName());

                var itemsStockRejectedDescription = string.Join(", ", itemsStockRejectedProductNames);
                _description = $"The product items don't have stock: ({itemsStockRejectedDescription}).";
            }
        }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new OrderingDomainException($"Is not possible to change the order status from {OrderStatus.Name} to {orderStatusToChange.Name}.");
        }
    }

    /// <summary>
    /// 订单明细
    /// </summary>
    public partial class Order
    {
        private readonly List<OrderItem> _orderItems;

        public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            var existingOrderForProduct = _orderItems.Where(o => o.ProductId == productId).SingleOrDefault();
            if (existingOrderForProduct != null)
            {
                //if previous line exist modify it with higher discount  and units..
                if (discount > existingOrderForProduct.GetCurrentDiscount())
                {
                    existingOrderForProduct.SetNewDiscount(discount);
                }
                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                //add validated new order item
                var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
                _orderItems.Add(orderItem);
            }
        }
    }
}