//using Basket.Api.Services;
//using Basket.Application.Events;
//using Basket.Application.Models;
//using LinFx.Extensions.EventBus;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Microsoft.eShopOnContainers.Services.Basket.API.IntegrationEvents.EventHandling
//{
//    public class ProductPriceChangedConsumer : IIntegrationEventHandler<ProductPriceChanged>
//    {
//        private readonly IBasketRepository _repository;

//        public ProductPriceChangedConsumer(IBasketRepository repository)
//        {
//            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        }

//        public Task Handle(ProductPriceChanged @event)
//        {
//            //var userIds = _repository.GetUsers();
//            //foreach (var id in userIds)
//            //{
//            //    var basket = await _repository.GetBasketAsync(id);

//            //    await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice, basket);                      
//            //}
//            throw new NotImplementedException();
//        }

//        private async Task UpdatePriceInBasketItems(int productId, decimal newPrice, decimal oldPrice, CustomerBasket basket)
//        {
//            string match = productId.ToString();
//            var itemsToUpdate = basket?.Items?.Where(x => x.ProductId == match).ToList();

//            if (itemsToUpdate != null)
//            {
//                foreach (var item in itemsToUpdate)
//                {
//                    if(item.UnitPrice == oldPrice)
//                    { 
//                        var originalPrice = item.UnitPrice;
//                        item.UnitPrice = newPrice;
//                        item.OldUnitPrice = originalPrice;
//                    }
//                }
//                await _repository.UpdateBasketAsync(basket);
//            }         
//        }
//    }
//}

