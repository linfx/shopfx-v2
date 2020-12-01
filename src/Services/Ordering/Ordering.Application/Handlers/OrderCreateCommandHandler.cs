using LinFx.Data.Abstractions;
using LinFx.Extensions.Mediator.Idempotency;
using MediatR;
using Ordering.Domain.Models.OrderAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 订单创建命令处理
    /// </summary>
    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, bool>
    {
        private readonly IRepository<Order> _orderRepository;
        //private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

        public OrderCreateCommandHandler(
            IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
            //_orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        }

        public async Task<bool> Handle(OrderCreateCommand message, CancellationToken cancellationToken)
        {
            // Add Integration event to clean the basket
            //var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(message.UserId);
            //await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            // Add/Update the Buyer AggregateRoot
            // DDD patterns comment: Add child entities and value-objects through the Order Aggregate-Root
            // methods and constructor so validations, invariants and business logic 
            // make sure that consistency is preserved across the whole aggregate
            var address = new Address(message.Street, message.City, message.State, message.Country, message.ZipCode);
            var order = new Order(message.UserId, message.UserName, address, message.CardTypeId, message.CardNumber, message.CardSecurityNumber, message.CardHolderName, message.CardExpiration);

            foreach (var item in message.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }

            _orderRepository.Add(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }

    // Use for Idempotency in Command process
    public class CreateOrderIdentifiedCommandHandler : IdentifiedCommandHandler<OrderCreateCommand, bool>
    {
        public CreateOrderIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager) { }

        protected override bool CreateResultForDuplicateRequest() => true;
    }
}
