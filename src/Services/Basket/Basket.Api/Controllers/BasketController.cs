using Basket.Api.Services;
using Basket.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    /// <summary>
    /// 购物车Api
    /// </summary>
    [ApiController]
    [Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _repository;
        //private readonly IHttpContextPrincipalAccessor _identitySvc;
        //private readonly IEventBus _eventBus;

        //public BasketController(IBasketRepository repository,
        //    IHttpContextPrincipalAccessor identityService,
        //    IEventBus eventBus)
        //{
        //    _repository = repository;
        //    _identitySvc = identityService;
        //    _eventBus = eventBus;
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.GetBasketAsync(id);
            if (basket == null)
            {
                return Ok(new CustomerBasket(id) { });
            }
            return Ok(basket);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody]CustomerBasket value)
        {
            var basket = await _repository.UpdateBasketAsync(value);
            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CustomerBasket value)
        {
            var basket = await _repository.GetBasketAsync(value.BuyerId);
            if (basket == null)
            {
                basket = new CustomerBasket(value.BuyerId);
            }

            foreach (var item in value.Items)
            {
                var tmp = basket.Items.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (tmp == null)
                {
                    basket.Items.Add(item);
                }
                else
                {
                    tmp.Quantity++;
                }
            }

            await _repository.UpdateBasketAsync(basket);
            return Ok(basket);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _repository.DeleteBasketAsync(id);
        }
    }
}