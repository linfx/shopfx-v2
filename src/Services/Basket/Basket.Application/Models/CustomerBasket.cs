using System.Collections.Generic;

namespace Basket.Application.Models
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class CustomerBasket
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public string BuyerId { get; set; }

        /// <summary>
        /// 购物车明细
        /// </summary>
        public List<BasketItem> Items { get; set; }

        /// <summary>
        /// 购物车
        /// </summary>
        /// <param name="customerId">客户Id</param>
        public CustomerBasket(string customerId)
        {
            BuyerId = customerId;
            Items = new List<BasketItem>();
        }
    }
}
