namespace Basket.Application.Events
{
    /// <summary>
    /// 商品价格调整
    /// </summary>
    public class ProductPriceChanged
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public long ProductId { get; private set; }

        /// <summary>
        /// 新价格
        /// </summary>
        public decimal NewPrice { get; private set; }

        /// <summary>
        /// 旧价格
        /// </summary>
        public decimal OldPrice { get; private set; }

        public ProductPriceChanged(long productId, decimal newPrice, decimal oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
