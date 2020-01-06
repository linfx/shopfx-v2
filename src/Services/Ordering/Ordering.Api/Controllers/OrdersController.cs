using LinFx.Extensions.Mediator.Idempotency;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Domain.Commands;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.Api.Controllers
{
    /// <summary>
    /// 订单Api
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Route("")]
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> Get()
        //{
        //    var userid = _httpContextPrincipalAccessor.Principal.FindUserId();
        //    var orders = await _orderService.GetOrdersFromUserAsync(userid);
        //    return Ok(orders);
        //}

        ///// <summary>
        ///// 获取订单
        ///// </summary>
        ///// <param name="orderId"></param>
        ///// <returns></returns>
        //[Route("{orderId:int}")]
        //[HttpGet]
        //[ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetOrder(int orderId)
        //{
        //    try
        //    {
        //        var order = await _orderService.GetOrderAsync(orderId);
        //        return Ok(order);
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //}

        //[Route("cardTypes")]
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<CardType>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetCardTypes()
        //{
        //    var cardTypes = await _orderService.GetCardTypesAsync();
        //    return Ok(cardTypes);
        //}

        /// <summary>
        /// 订单取消
        /// </summary>
        /// <param name="command"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPut]
        [HttpPut("cancel")]
        public async Task<IActionResult> CancelOrder(CancelOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CancelOrderCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return commandResult ? Ok() : (IActionResult)BadRequest();
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name = "command" ></param >
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ship")]
        public async Task<IActionResult> ShipOrder(ShipOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<ShipOrderCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return commandResult ? Ok() : (IActionResult)BadRequest();
        }

        /// <summary>
        /// 订单预览
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("draft")]
        public async Task<IActionResult> GetOrderDraftFromBasketData(CreateOrderDraftCommand command)
        {
            var draft = await _mediator.Send(command);
            return Ok(draft);
        }
    }
}
