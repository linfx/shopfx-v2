using LinFx.Extensions.EventBus;
using System.Collections.Generic;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderStockRejectedEvent : Event
    {
        public long OrderId { get; }

        public List<ConfirmedOrderStockItem> OrderStockItems { get; }

        public OrderStockRejectedEvent(long orderId, List<ConfirmedOrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }

    public class ConfirmedOrderStockItem
    {
        public long ProductId { get; }

        public bool HasStock { get; }

        public ConfirmedOrderStockItem(long productId, bool hasStock)
        {
            ProductId = productId;
            HasStock = hasStock;
        }
    }
}