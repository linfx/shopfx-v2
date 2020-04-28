using Basket.Api.Services;
using Basket.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        private readonly IBasketRepository _basketService;

        public BasketController(IBasketRepository basketService)
        {
            _basketService = basketService;
        }

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="id">购物车Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _basketService.GetAsync(id);
            if (basket == null)
                return Ok(new CustomerBasket(id));

            return Ok(basket);
        }

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="input">购物车</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(CustomerBasket), 200)]
        public async Task<IActionResult> Put(CustomerBasket input)
        {
            var basket = await _basketService.UpdateAsync(input);
            return Ok(basket);
        }

        /// <summary>
        /// 新增购物车
        /// </summary>
        /// <param name="input">购物车</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), 200)]
        public async Task<IActionResult> Post(CustomerBasket input)
        {
            var basket = await _basketService.GetAsync(input.BuyerId);
            if (basket == null)
                basket = new CustomerBasket(input.BuyerId);

            foreach (var item in input.Items)
            {
                var tmp = basket.Items.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (tmp == null)
                    basket.Items.Add(item);
                else
                    tmp.Quantity++;
            }

            await _basketService.UpdateAsync(basket);
            return Ok(basket);
        }

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="id">购物车Id</param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _basketService.DeleteAsync(id);
        }
    }
}