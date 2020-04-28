using Basket.Api.Services;
using Basket.Application.Models;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Application.Events
{
    /// <summary>
    /// 价格变动通知
    /// </summary>
    public class ProductPriceChangedConsumer : IConsumer<ProductPriceChanged>
    {
        private readonly IBasketRepository _repository;

        public ProductPriceChangedConsumer(IBasketRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<ProductPriceChanged> context)
        {
            //var userIds = _repository.GetUsers();
            //foreach (var id in userIds)
            //{
            //    var basket = await _repository.GetBasketAsync(id);

            //    await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice, basket);                      
            //}
            throw new NotImplementedException();
        }

        private async Task UpdatePriceInBasketItems(long productId, decimal newPrice, decimal oldPrice, CustomerBasket basket)
        {
            string match = productId.ToString();
            var itemsToUpdate = basket?.Items?.Where(x => x.ProductId == match).ToList();
            if (itemsToUpdate != null)
            {
                foreach (var item in itemsToUpdate)
                {
                    if (item.UnitPrice == oldPrice)
                    {
                        var originalPrice = item.UnitPrice;
                        item.UnitPrice = newPrice;
                        item.OldUnitPrice = originalPrice;
                    }
                }
                await _repository.UpdateAsync(basket);
            }
        }
    }
}

