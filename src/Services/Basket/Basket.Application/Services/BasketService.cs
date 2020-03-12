using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using LinFx.Utils;
using Basket.Application.Models;

namespace Basket.Api.Services
{
    public class BasketService : IBasketService
    {
        private readonly ILogger _logger;
        private readonly IDistributedCache _cache;

        public BasketService(
            ILoggerFactory loggerFactory,
            IDistributedCache cache)
        {
            _logger = loggerFactory.CreateLogger<BasketService>();
            _cache = cache;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            await _cache.RemoveAsync(id);
            return true;
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var data = await _cache.GetStringAsync(customerId);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            return data.ToObject<CustomerBasket>();
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var cache = await GetBasketAsync(basket.BuyerId);

            //basket.Items.ForEach(p =>
            //{
            //    var tmp = cache.Items.FirstOrDefault(x => x.ProductId == p.ProductId && x.Quantity != p.Quantity);
            //    if (tmp != null)
            //    {
            //        tmp.Quantity = p.Quantity;
            //    }
            //});

            await _cache.SetStringAsync(basket.BuyerId, basket.ToJson());

            _logger.LogInformation("Basket item persisted succesfully.");

            return cache;
        }
    }
}
