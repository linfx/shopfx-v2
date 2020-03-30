using LinFx.Extensions.Mediator.Idempotency;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Events;
using Ordering.Domain.Commands;
using System;
using System.Threading.Tasks;
using IMediator = MediatR.IMediator;

namespace Ordering.Application.Handlers
{
    /// <summary>
    /// 用户结算消息
    /// </summary>
    public class UserCheckoutAcceptedConsumer : IConsumer<UserCheckoutAccepted>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public UserCheckoutAcceptedConsumer(
            ILogger<UserCheckoutAcceptedConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<UserCheckoutAccepted> context)
        {
            var result = false;
            var eventMsg = context.Message;
            if (eventMsg.RequestId != Guid.Empty)
            {
                var createOrderCommand = new CreateOrderCommand(eventMsg.Basket.Items, eventMsg.UserId, eventMsg.UserName, 
                    eventMsg.City, eventMsg.Street,
                    eventMsg.State, eventMsg.Country, eventMsg.ZipCode,
                    eventMsg.CardNumber, eventMsg.CardHolderName, eventMsg.CardExpiration,
                    eventMsg.CardSecurityNumber, eventMsg.CardTypeId);

                var requestCreateOrder = new IdentifiedCommand<CreateOrderCommand, bool>(createOrderCommand, eventMsg.RequestId);
                result = await _mediator.Send(requestCreateOrder);
            }

            _logger.LogTrace(result ?
                $"UserCheckoutAccepted integration event has been received and a create new order process is started with requestId: {eventMsg.RequestId}" :
                $"UserCheckoutAccepted integration event has been received but a new order process has failed with requestId: {eventMsg.RequestId}");
        }
    }
}