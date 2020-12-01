using LinFx.Data.Abstractions;
using LinFx.Data.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Events;
using Ordering.Domain.Events;
using Ordering.Domain.Models.BuyerAggregate;
using Ordering.Domain.Models.OrderAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Application.DomainEventHandlers.OrderStockConfirmed
{
    public class OrderStatusChangedToStockConfirmedDomainEventHandler : INotificationHandler<OrderStatusChangedToStockConfirmedDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Buyer> _buyerRepository;
        //private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

        public OrderStatusChangedToStockConfirmedDomainEventHandler(
            ILogger logger,
            IRepository<Order> orderRepository,
            IRepository<Buyer> buyerRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _buyerRepository = buyerRepository;
        }

        public async Task Handle(OrderStatusChangedToStockConfirmedDomainEvent orderStatusChangedToStockConfirmedDomainEvent, CancellationToken cancellationToken)
        {
            _logger.LogTrace(
                $"Order with Id: {orderStatusChangedToStockConfirmedDomainEvent.OrderId} has been successfully updated with " +
                $"a status order id: {OrderStatus.StockConfirmed.Id}");

            var order = await _orderRepository.FirstOrDefaultAsync(orderStatusChangedToStockConfirmedDomainEvent.OrderId);
            var buyer = await _buyerRepository.FirstOrDefaultAsync(order.GetBuyerId.Value);

            var orderStatusChangedToStockConfirmedIntegrationEvent = new OrderStatusChangedToStockConfirmedEvent(order.Id, order.OrderStatus.Name, buyer.Name);
            //await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStatusChangedToStockConfirmedIntegrationEvent);
        }
    }
}