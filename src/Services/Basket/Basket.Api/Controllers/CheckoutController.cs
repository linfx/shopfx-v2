using Basket.Api.Services;
using Basket.Application.Events;
using Basket.Application.Models;
using LinFx.Extensions.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    /// <summary>
    /// 结算Api
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly IBasketRepository _basketService;

        public CheckoutController(
            IEventBus eventBus,
            IBasketRepository basketService)
        {
            _eventBus = eventBus;
            _basketService = basketService;
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="basketCheckout">结算表单</param>
        /// <param name="requestId">请求Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Checkout(BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        {
            basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ? guid : basketCheckout.RequestId;

            var basket = await _basketService.GetAsync(User.Identity.Name);
            if (basket == null)
                return BadRequest();

            //var userName = User.Identity.Name;

            //var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
            //    basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
            //    basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket);

            //// Once basket is checkout, sends an integration event to
            //// ordering.api to convert basket to order and proceeds with
            //// order creation process
            //await _eventBus.PublishAsync(eventMessage);

            var eventMsg = basketCheckout.MapTo<UserCheckoutAccepted>();
            eventMsg.Basket = basket;

            return Accepted();
        }
    }
}
