namespace Basket.Application.Models
{
    /// <summary>
    /// 购物车明细
    /// </summary>
    public class BasketItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 旧价格
        /// </summary>
        public decimal OldUnitPrice { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string PictureUrl { get; set; }
    }
}
