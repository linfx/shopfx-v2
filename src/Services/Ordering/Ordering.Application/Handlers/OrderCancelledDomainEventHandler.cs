using LinFx.Data;
using LinFx.Data.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.Domain.Events;
using Ordering.Domain.Models.BuyerAggregate;
using Ordering.Domain.Models.OrderAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    /// <summary>
    /// 订单取消处理
    /// </summary>
    public class OrderCancelledDomainEventHandler : INotificationHandler<OrderCancelledDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly IMqService _eventBus;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Buyer> _buyerRepository;

        public OrderCancelledDomainEventHandler(ILoggerFactory logger,
            IMqService eventBus,
            IRepository<Order> orderRepository,
            IRepository<Buyer> buyerRepository)
        {
            _logger = logger;
            _eventBus = eventBus;
            _orderRepository = orderRepository;
            _buyerRepository = buyerRepository;
        }

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
