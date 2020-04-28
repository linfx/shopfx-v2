using LinFx.Data.Abstractions;
using LinFx.Data.Linq;
using LinFx.Extensions.EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.Domain.Events;
using Ordering.Domain.Models.BuyerAggregate;
using Ordering.Domain.Models.OrderAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Application.DomainEventHandlers.OrderCancelled
{
    /// <summary>
    /// 订单取消处理
    /// </summary>
    public class OrderCancelledDomainEventHandler : INotificationHandler<OrderCancelledDomainEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Buyer> _buyerRepository;
        private readonly ILoggerFactory _logger;
        private readonly IMqService _eventBus;

        public async Task Handle(OrderCancelledDomainEvent orderCancelledDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger(nameof(OrderCancelledDomainEvent))
             .LogTrace($"Order with Id: {orderCancelledDomainEvent.Order.Id} has been successfully updated with " +
                       $"a status order id: {OrderStatus.Shipped.Id}");

            var order = await _orderRepository.FirstOrDefaultAsync(orderCancelledDomainEvent.Order.Id);
            var buyer = await _buyerRepository.FirstOrDefaultAsync(order.GetBuyerId.Value);

            var orderStatusChangedToCancelledIntegrationEvent = new OrderStatusChangedToCancelledEvent(order.Id, order.OrderStatus.Name, buyer.Name);
            await _eventBus.SendAsync("fff", orderStatusChangedToCancelledIntegrationEvent);
        }
    }
}
