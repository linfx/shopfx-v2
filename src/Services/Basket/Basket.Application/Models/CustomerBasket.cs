using System.Collections.Generic;

namespace Basket.Application.Models
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class CustomerBasket
    {
        public string BuyerId { get; set; }

        public List<BasketItem> Items { get; set; }

        public CustomerBasket(string customerId)
        {
            BuyerId = customerId;
            Items = new List<BasketItem>();
        }
    }
}
