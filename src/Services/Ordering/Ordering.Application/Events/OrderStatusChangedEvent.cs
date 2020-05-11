using System.Collections.Generic;
using LinFx.Extensions.EventBus;

namespace Ordering.Application.Events
{
    public class OrderStatusChangedToAwaitingValidationEvent : Event
    {
        public long OrderId { get; }

        public string OrderStatus { get; }

        public string BuyerName { get; }

        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToAwaitingValidationEvent(long orderId, string orderStatus, string buyerName, IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }

    public class OrderStatusChangedToPaidEvent : Event
    {
        public long OrderId { get; }

        public string OrderStatus { get; }

        public string BuyerName { get; }

        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToPaidEvent(long orderId,
            string orderStatus,
            string buyerName,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }

    public class OrderStatusChangedToShippedEvent : Event
    {
        public long OrderId { get; }

        public string OrderStatus { get; }

        public string BuyerName { get; }

        public OrderStatusChangedToShippedEvent(long orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }

    public class OrderStatusChangedToStockConfirmedEvent : Event
    {
        public long OrderId { get; }

        public string OrderStatus { get; }

        public string BuyerName { get; }

        public OrderStatusChangedToStockConfirmedEvent(long orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }

    public class OrderStatusChangedToSubmittedEvent : Event
    {
        public long OrderId { get; }

        public string OrderStatus { get; }

        public string BuyerName { get; }

        public OrderStatusChangedToSubmittedEvent(long orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }

    public class OrderStockItem
    {
        public long ProductId { get; }

        public int Units { get; }

        public OrderStockItem(long productId, int units)
        {
            ProductId = productId;
            Units = units;
        }
    }
}