using LinFx.Extensions.Mediator.Idempotency;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Commands;
using System;
using System.Threading.Tasks;
using IMediator = MediatR.IMediator;

namespace Ordering.Application.Events
{
    /// <summary>
    /// 结算消费
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
                // 创建订单指令
                var createOrderCommand = new OrderCreateCommand(eventMsg.Basket.Items, eventMsg.UserId, eventMsg.UserName,
                    eventMsg.City, eventMsg.Street,
                    eventMsg.State, eventMsg.Country, eventMsg.ZipCode,
                    eventMsg.CardNumber, eventMsg.CardHolderName, eventMsg.CardExpiration,
                    eventMsg.CardSecurityNumber, eventMsg.CardTypeId);

                // 请求创建订单
                var requestCreateOrder = new IdentifiedCommand<OrderCreateCommand, bool>(eventMsg.RequestId, createOrderCommand);
                result = await _mediator.Send(requestCreateOrder);
            }

            _logger.LogTrace(result ?
                $"UserCheckoutAccepted integration event has been received and a create new order process is started with requestId: {eventMsg.RequestId}" :
                $"UserCheckoutAccepted integration event has been received but a new order process has failed with requestId: {eventMsg.RequestId}");
        }
    }
}